```mermaid
graph LR;
    Wander -- time passed --> LookAtAttraction
    LookAtAttraction -- time passed --> Wander

    Wander -- park breaks --> RunAround
    LookAtAttraction -- park breaks --> RunAround
    
    RunAround -- see player --> FollowPlayer
    
    RunAround -- see escape --> Escape
    FollowPlayer -- see escape --> Escape

```
