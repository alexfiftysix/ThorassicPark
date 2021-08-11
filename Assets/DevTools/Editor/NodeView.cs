using System;
using Characters.Brains;
using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;
using Characters.Brains.Transitions;
using DevTools.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public sealed class NodeView : Node
{
    public BrainNode node;
    public Port input;
    public Port output;
    public Action<NodeView> onNodeSelected;

    public NodeView(BrainNode node)
    {
        this.node = node;
        title = node.name;
        viewDataKey = node.guid;

        CreateInputPorts();
        CreateOutputPorts();

        style.left = node.position.x;
        style.top = node.position.y;
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        node.position = new Vector2(newPos.xMin, newPos.yMin);
    }

    public override void OnSelected()
    {
        base.OnSelected();

        if (onNodeSelected != null)
        {
            onNodeSelected.Invoke(this);
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
}