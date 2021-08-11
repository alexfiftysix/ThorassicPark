using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Characters.Brains;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
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

        foreach (var brainState in _brain.states)
        {
            CreateNodeView(brainState);
        }
    }

    private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
    {
        if (graphviewchange.elementsToRemove != null)
        {
            graphviewchange.elementsToRemove.ForEach(e =>
            {
                if (e is NodeView nodeView)
                {
                    _brain.DeleteNode(nodeView.node);
                }
            });
        }

        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // Don't need any subtypes here - States and Decisions are just containers 
        evt.menu.AppendAction("State", action => CreateNode<BrainState>());
        evt.menu.AppendAction("Transition", action => CreateNode<BrainTransition>());
        // TODO: They don't get created at the mouse pos, which is a bit of a problem
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
                                   || startNodeView.node is BrainTransition && endNodeView.node is BrainState);
                    }
                    return true;
                }
            )
            .ToList();
    }

    private void CreateNode<T>() where T : BrainNode
    {
        var node = _brain.CreateNode<T>();
        CreateNodeView(node);
    }

    private void CreateNodeView(BrainNode state)
    {
        var nodeView = new NodeView(state);
        nodeView.onNodeSelected = onNodeSelected;
        AddElement(nodeView);
    }
}