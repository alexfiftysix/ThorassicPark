using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Characters.Brains
{
    [CreateAssetMenu(menuName = "Brains/Brain")]
    public class Brain : ScriptableObject
    {
        public BrainNode initialState;
        // Needed because nodes can be detached
        public List<BrainNode> states;

        public BrainNode CreateNode<T>() where T : BrainNode
        {
            var newNode = CreateInstance(typeof(T)) as T;
            newNode.name = $"New {typeof(T).ToString().Split('.').Last()}";
            newNode.guid = GUID.Generate().ToString();
            states.Add(newNode);

            AssetDatabase.AddObjectToAsset(newNode, this);
            AssetDatabase.SaveAssets();

            return newNode;
        }

        public void DeleteNode(BrainNode node)
        {
            states.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }
    }
}