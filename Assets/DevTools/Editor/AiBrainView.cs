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
    private Brain _brain;
    public Action<NodeView> onNodeSelected;

    public new class UxmlFactory : UxmlFactory<AiBrainView, UxmlTraits>
    {
    }

    public AiBrainView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer { maxScale = 10000, minScale = 0.1f });
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());

        var styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/DevTools/Editor/AiBrainEditor.uss");
        styleSheets.Add(styleSheet);
    }

    public void PopulateView(Brain brain)
    {
        _brain = brain;
        if (brain.rootNode == null)
        {
            brain.rootNode = brain.CreateNode<RootNode>(Vector2.zero);
        }

        graphViewChanged -= OnGraphViewChanged;
        DeleteElements(graphElements.ToList()); // The Queryable doesn't contain a definition for Where() apparently
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

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphViewChange)
    {
        if (graphViewChange.elementsToRemove != null)
        {
            foreach (var element in graphViewChange.elementsToRemove)
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

        if (graphViewChange.edgesToCreate != null)
        {
            foreach (var edge in graphViewChange.edgesToCreate)
            {
                if (edge.output.node is NodeView parentView && edge.input.node is NodeView childView)
                    Brain.AddChild(parentView.node, childView.node);
            }
        }

        return graphViewChange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        Vector3 screenMousePosition = evt.localMousePosition;
        Vector2 worldMousePosition = (screenMousePosition - contentViewContainer.transform.position) *
                                     (1 / contentViewContainer.transform.scale.x);

        // Don't need any subtypes here - States and Decisions are just containers
        // If you want subtypes, consider TypeCache.GetTypesDerivedFrom<BrainNode>(), to get all types.
        evt.menu.AppendAction("State", action => CreateNode<BrainState>(worldMousePosition));
        evt.menu.AppendAction("Transition", action => CreateNode<BrainTransition>(worldMousePosition));
        evt.menu.AppendAction("Root", action => CreateNode<RootNode>(worldMousePosition));
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports
            .ToList() // Unity claims that UQueryState doesn't support Where()
            .Where(endPort =>
                {
                    if (startPort.node is NodeView startNodeView
                        && endPort.node is NodeView endNodeView
                        && endPort.direction != startPort.direction
                        && endPort.node != startPort.node)
                    {
                        return startNodeView.node.CanConnectTo(endNodeView.node);
                    }

                    return false;
                }
            )
            .ToList();
    }

    // ReSharper disable once MemberCanBePrivate.Global
    // Keeping this public because there's some reflection using it in 'BuildContextualMenu,' and I don't understand that well enough to keep this private  
    public void CreateNode<T>(Vector2 mousePos) where T : BrainNode
    {
        // TODO: Focus node after create. Focus description field.
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