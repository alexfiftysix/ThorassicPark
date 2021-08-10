using Characters.Brains.BrainStates;
using UnityEditor.Experimental.GraphView;

public sealed class NodeView : Node
{
    public BrainState state;

    public NodeView(BrainState state)
    {
        this.state = state;
        title = state.name;
    }
}