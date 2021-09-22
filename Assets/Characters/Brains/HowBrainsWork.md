A brain contains a list of 
- States + Action + Transitions

Every State is related to one or more actions that are done every frame.
Each Transition contains 1 or more Decisions. If any decision is true, progress to the next state

Brain gets plugged into an agent (brain can be AI or player)
Actions use the delegate pattern to affect the Controllable agent