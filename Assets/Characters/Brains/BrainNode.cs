using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Characters.Brains
{
    public abstract class BrainNode : ScriptableObject
    {
        // Only used in the graph editor
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        [TextArea] public string description;

        public abstract List<BrainNode> GetChildren();

        public abstract bool CanConnectTo(BrainNode other);

        public abstract void AddExtras(IMGUIContainer addContainer);
    }
}