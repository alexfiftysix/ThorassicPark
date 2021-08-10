using Characters.Brains.BrainStates;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public sealed class NodeView : Node
{
    public BrainState state;

    public NodeView(BrainState state)
    {
        this.state = state;
        title = state.name;
        viewDataKey = state.guid;

        style.left = state.position.x;
        style.top = state.position.y;
    }

    public override void SetPosition(Rect newPos)
    {
        base.SetPosition(newPos);
        state.position.x = newPos.xMin;
        state.position.y = newPos.yMin;
    }
}