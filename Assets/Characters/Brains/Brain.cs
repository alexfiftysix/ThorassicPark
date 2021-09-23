﻿using System;
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

        public T CreateNode<T>(Vector2 mousePos) where T : BrainNode
        {
            if (CreateInstance(typeof(T)) is T newNode)
            {
                newNode.name = $"{typeof(T).ToString().Split('.').Last()}";
                newNode.guid = GUID.Generate().ToString();
                newNode.position = mousePos;
                states.Add(newNode);

                AssetDatabase.AddObjectToAsset(newNode, this);
                AssetDatabase.SaveAssets();

                return newNode;
            }

            throw new NullReferenceException("Node not created");
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
            // TODO: Something's going wrong here
            return parent.GetChildren();
        }
    }
}