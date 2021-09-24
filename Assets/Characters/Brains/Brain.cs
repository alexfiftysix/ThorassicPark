using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace Characters.Brains
{
    [CreateAssetMenu(menuName = "Brains/Brain")]
    public class Brain : ScriptableObject
    {
        public RootNode rootNode;

        // Needed because nodes can be detached
        [FormerlySerializedAs("states")] public List<BrainNode> nodes = new List<BrainNode>();

        public T CreateNode<T>(Vector2 mousePos) where T : BrainNode
        {
            if (CreateInstance(typeof(T)) is T newNode)
            {
                newNode.name = $"{typeof(T).ToString().Split('.').Last()}";
                newNode.description = newNode.name;
                newNode.guid = GUID.Generate().ToString();
                newNode.position = mousePos;
                nodes.Add(newNode);
                if (newNode is RootNode newRootNode) rootNode = newRootNode;

                AssetDatabase.AddObjectToAsset(newNode, this);
                AssetDatabase.SaveAssets();

                return newNode;
            }

            throw new NullReferenceException("Node not created");
        }

        public void DeleteNode(BrainNode node)
        {
            nodes.Remove(node);
            if (node is RootNode) rootNode = null;
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();
        }

        public static void AddChild(BrainNode parent, BrainNode child)
        {
            switch (parent)
            {
                case BrainState brainState when child is BrainTransition transition:
                    brainState.AddTransition(transition);
                    break;
                case BrainTransition transition when child is BrainState brainState:
                    transition.SetNextState(brainState);
                    break;
                case RootNode rootNode when child is BrainState brainState:
                    rootNode.SetStartState(brainState);
                    break;
            }
        }

        public static void RemoveChild(BrainNode parent, BrainNode child)
        {
            switch (parent)
            {
                case BrainState brainState when child is BrainTransition transition:
                    brainState.RemoveTransition(transition);
                    break;
                case BrainTransition transition when child is BrainState:
                    transition.SetNextState(null);
                    break;
                case RootNode rootNode when child is BrainState:
                    rootNode.SetStartState(null);
                    break;
            }
        }

        public static List<BrainNode> GetChildren(BrainNode parent)
        {
            return parent.GetChildren();
        }
    }
}