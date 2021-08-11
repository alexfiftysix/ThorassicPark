using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;
using Characters.Brains.Transitions;
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

        public void AddChild(BrainNode parent, BrainNode child)
        {
            switch (parent)
            {
                case BrainState brainState when child is BrainTransition transition:
                    brainState.AddTransition(transition);
                    break;
                case BrainTransition transition when child is BrainState brainState:
                    transition.SetNextState(brainState);
                    break;
            }
        }

        public void RemoveChild(BrainNode parent, BrainNode child)
        {
            switch (parent)
            {
                case BrainState brainState when child is BrainTransition transition:
                    brainState.RemoveTransition(transition);
                    break;
                case BrainTransition transition when child is BrainState brainState:
                    transition.SetNextState(null);
                    break;
            }
        }

        public List<BrainNode> GetChildren(BrainNode parent)
        {
            return parent switch
            {
                BrainState brainState => brainState.GetTransitions(),
                BrainTransition transition => new List<BrainNode>() {transition.nextState},
                _ => throw new NotSupportedException(
                    "GetChildren only supports BrainState and BrainTransition right now")
            };
        }
    }
}