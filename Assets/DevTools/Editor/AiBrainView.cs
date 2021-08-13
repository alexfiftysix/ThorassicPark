using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Brains;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
// Don't want the nesting in the editor that the namespace brings
public class AiBrainView : GraphView
{
    // TODO: Need a RootNode as an entry point to the graph
    private Brain _brain;
    public Action<NodeView> onNodeSelected;

    public new class UxmlFactory : UxmlFactory<AiBrainView, UxmlTraits>
    {
    }

    public AiBrainView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer {maxScale = 10000, minScale = 0.1f});
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DevTools/Editor/AiBrainEditor.uss");
        styleSheets.Add(styleSheet);
    }

    public void PopulateView(Brain brain)
    {
        _brain = brain;

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements);
        graphViewChanged += OnGraphViewChanged;

        // Load nodes
        foreach (var brainState in _brain.states)
        {
            CreateNodeView(brainState);
        }
        
        // Load edges 
        foreach (var parentNode in _brain.states)
        {
            var children = Brain.GetChildren(parentNode);
            foreach (var childNode in children)
            {
                var parentView = FindNodeView(parentNode);
                var childView = FindNodeView(childNode);
                
                if (parentView != null && childView != null)
                {
                    var edge = parentView.output.ConnectTo(childView.input);
                    AddElement(edge);
                }
            }
        }
    }

    [CanBeNull]
    private NodeView FindNodeView([CanBeNull] BrainNode node)
    {
        if (node is null) return null;
        return GetNodeByGuid(node.guid) as NodeView;
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        if (graphviewchange.elementsToRemove != null)
        {
            foreach (var element in graphviewchange.elementsToRemove)
            {
                if (element is NodeView nodeView)
                {
                    _brain.DeleteNode(nodeView.node);
                }

                if (element is Edge edge)
                {
                    if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                        Brain.RemoveChild(parentView.node, childView.node);
                }
            }
        }

        if (graphviewchange.edgesToCreate != null)
        {
            foreach (var edge in graphviewchange.edgesToCreate)
            {
                if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                    Brain.AddChild(parentView.node, childView.node);
            }
        }

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        var mousePos = evt.localMousePosition; // TODO: This isn't quite right. With a reference to the editor window you should be able to convert mousePosition into a relative position
        
        // Don't need any subtypes here - States and Decisions are just containers
        // If you want subtypes, consider TypeCache.GetTypesDerivedFrom<BrainNode>(), to get all types.
        evt.menu.AppendAction("State", action => CreateNode<BrainState>(mousePos));
        evt.menu.AppendAction("Transition", action => CreateNode<BrainTransition>(mousePos));
        evt.menu.AppendAction("Root", action => CreateNode<RootNode>(mousePos));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        // TODO: Ugly
        return ports
            .Where(endPort =>
                {
                    if (startPort.node is NodeView startNodeView
                        && endPort.node is NodeView endNodeView
                    )
                    {
                        return endPort.direction != startPort.direction
                               && endPort.node != startPort.node
                               && (startNodeView.node is BrainState && endNodeView.node is BrainTransition
                                   || startNodeView.node is BrainTransition && endNodeView.node is BrainState
                                   || startNodeView.node is RootNode && endNodeView.node is BrainState);
                    }

                    return true;
                }
            )
            .ToList();
    }

    private void CreateNode<T>(Vector2 mousePos) where T : BrainNode
    {
        var node = _brain.CreateNode<T>(mousePos);
        CreateNodeView(node);
    }

    private void CreateNodeView(BrainNode state)
    {
        var nodeView = new NodeView(state);
        nodeView.onNodeSelected = onNodeSelected;
        AddElement(nodeView);
    }
}