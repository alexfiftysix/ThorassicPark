using UnityEngine;

namespace Characters.Brains
{
    public abstract class BrainNode : ScriptableObject
    {
        // Only used in the graph editor
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [TextArea] public string description;
    }
}