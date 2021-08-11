using System;
using Characters.Brains;
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
        input = InstantiatePort(Orientation.Horizontal, Direction.Input, Port.Capacity.Multi, typeof(bool));
        input.portName = string.Empty;
        inputContainer.Add(input);
    }
    
    private void CreateOutputPorts()
    {
        // bool not used, is a placeholder for nothing (void not allowed)
        output = InstantiatePort(Orientation.Horizontal, Direction.Output, Port.Capacity.Multi, typeof(bool));
        output.portName = string.Empty;
        outputContainer.Add(output);
    }
}