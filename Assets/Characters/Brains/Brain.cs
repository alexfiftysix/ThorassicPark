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
        public RootNode rootNode;

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
                case BrainTransition transition when child is BrainState brainState:
                    transition.SetNextState(null);
                    break;
                case RootNode rootNode when child is BrainState:
                    rootNode.SetStartState(null);
                    break;
            }
        }

        public static List<BrainNode> GetChildren(BrainNode parent)
        {
            return parent switch
            {
                BrainState brainState => brainState.GetTransitions(),
                BrainTransition transition => new List<BrainNode> {transition.nextState},
                RootNode rootNode => new List<BrainNode> {rootNode.startState},
                _ => throw new NotSupportedException(
                    $"GetChildren does not support node {parent.GetType()}")
            };
        }
    }
}