using System;
using Characters.Brains;
using Characters.Brains.BrainActions;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class NodeView : Node
{
    public BrainNode node;
    public Port input;
    public Port output;
    public Action<NodeView> onNodeSelected;

    public NodeView(BrainNode node) : base("Assets/DevTools/Editor/NodeView.uxml")
    {
        this.node = node;
        title = GetName(node);
        viewDataKey = node.guid;

        style.left = node.position.x;
        style.top = node.position.y;

        CreateInputPorts();
        CreateOutputPorts();
        SetupClasses();

        // Bind description tag
        var descriptionLabel = this.Q<Label>("description"); // name of UI element
        descriptionLabel.bindingPath = "description";  // name of property on BrainNode class
        if (node.description == string.Empty) node.description = "description";
        descriptionLabel.Bind(new SerializedObject(node));
        
        // Add + button
        var buttonContainer = this.Q<IMGUIContainer>("button-container");
        buttonContainer.Add(node.GetAddButton(buttonContainer));
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position = new Vector2(newPos.xMin, newPos.yMin);
    }

    public override void OnSelected()
    {
        base.OnSelected();
        onNodeSelected?.Invoke(this);
    }

    private void SetupClasses()
    {
        switch (node)
        {
            case RootNode _:
                AddToClassList("root");
                break;
            case BrainState _:
                AddToClassList("state");
                break;
            case BrainTransition _:
                AddToClassList("transition");
                break;
            case BrainAction _:
                AddToClassList("action");
                break;
        }
    }

    private void CreateInputPorts()
    {
        // bool not used, is a placeholder for nothing (void not allowed)
        if (!(node is RootNode))
        {
            input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));    
        }

        if (input != null)
        {
            input.portName = string.Empty;
            inputContainer.Add(input);
        }
    }

    private void CreateOutputPorts()
    {
        if (node is BrainAction)
        {
            return;
        }
        
        output = node switch
        {
            BrainState _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Multi,
                typeof(bool)),
            BrainTransition _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single,
                typeof(bool)),
            RootNode _ => InstantiatePort(
                Orientation.Horizontal,
                Direction.Output,
                Port.Capacity.Single,
                typeof(bool)),
            _ => output
        };

        output.portName = string.Empty;
        outputContainer.Add(output);
    }

    private static string GetName(BrainNode aNode)
    {
        return aNode switch
        {
            BrainState _ => "State",
            BrainAction _ => "Action",
            BrainTransition _ => "Transition",
            RootNode _ => "Root",
            _ => "No Name"
        };
    }
}