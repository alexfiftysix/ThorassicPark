using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;

namespace Characters.Brains
{
    public class StateStack
    {
        private readonly List<BrainState> _states = new List<BrainState>();

        public void Push(BrainState brainState)
        {
            _states.Insert(0, brainState);
        }

        public void DoActions(ControllableBase controllable)
        {
            var maxIndex = _states.Count - 1;
            var currentIndex = 0;

            while (currentIndex <= maxIndex)
            {
                var currentState = _states.ElementAt(currentIndex);
                var nextState = currentState.DoActions(controllable);

                // If this state transitioned, all sub-brains below this state need to be removed
                if (nextState != null)
                {
                    var popCount = currentIndex;
                    for (var i = 0; i < popCount; i++)
                    {
                        _states.RemoveAt(0);
                    }

                    maxIndex -= popCount;
                    currentIndex = 0;
                    _states[currentIndex] = nextState;
                    nextState.Initialise(controllable);
                }

                // increment after resetting to 0 because otherwise you'll play the successful state twice
                currentIndex++;
            }
        }

        public void Initialise(ControllableBase controllable)
        {
            foreach (var state in _states)
            {
                state.Initialise(controllable);
            }
        }
    }
}