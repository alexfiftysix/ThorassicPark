using Characters.Brains.BrainStates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public sealed class NodeView : Node
{
    public BrainState state;
    public Port input;
    public Port output;

    public NodeView(BrainState state)
    {
        this.state = state;
        title = state.name;
        viewDataKey = state.guid;

        CreateInputPorts();
        CreateOutputPorts();
        
        style.left = state.position.x;
        style.top = state.position.y;
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        state.position.x = newPos.xMin;
        state.position.y = newPos.yMin;
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