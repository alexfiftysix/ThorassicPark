using System.Collections.Generic;
using Characters.Brains.BrainStates;
using UnityEditor;
using UnityEngine;

namespace Characters.Brains
{
    [CreateAssetMenu(menuName = "Brains/Brain")]
    public class Brain : ScriptableObject
    {
        public BrainState initialState;

        // Needed because nodes can be detached
        public List<BrainState> states;

        public BrainState CreateState()
        {
            var newState = CreateInstance(typeof(BrainState)) as BrainState;
            newState.name = "new state";
            newState.guid = GUID.Generate().ToString();
            states.Add(newState);

            AssetDatabase.AddObjectToAsset(newState, this);
            AssetDatabase.SaveAssets();

            return newState;
        }

        public void DeleteState(BrainState state)
        {
            states.Remove(state);
            AssetDatabase.RemoveObjectFromAsset(state);
            AssetDatabase.SaveAssets();
        }
    }
}