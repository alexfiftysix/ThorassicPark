using System.Collections.Generic;
using System.Linq;
using Characters.Brains;
using Characters.Brains.BrainStates;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

// ReSharper disable once CheckNamespace
// Don't want the nesting in the editor that the namespace brings
public class AiBrainView : GraphView
{
    private Brain _brain;

    public new class UxmlFactory : UxmlFactory<AiBrainView, UxmlTraits>
    {
    }

    public AiBrainView()
    {
        Insert(0, new GridBackground());

        this.AddManipulator(new ContentZoomer() {maxScale = 10000, minScale = 0.1f});
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
            // TODO: Nodes don't remember their position!
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
                    _brain.DeleteState(nodeView.state);
                }
            });
        }
        return graphviewchange;
    }

    public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
    {
        // Don't need any subtypes here - Actions and Decisions are added to a State, which is just a container 
        evt.menu.AppendAction("State", action => CreateState());
        // TODO: They don't get created at the mouse pos, which is a bit of a problem
    }

    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        return ports
            .Where(endPort => endPort.direction != startPort.direction
                              && endPort.node != startPort.node)
            .ToList();
    }

    private void CreateState()
    {
        var state = _brain.CreateState();
        CreateNodeView(state);
    }

    private void CreateNodeView(BrainState state)
    {
        var nodeView = new NodeView(state);
        AddElement(nodeView);
    }
}