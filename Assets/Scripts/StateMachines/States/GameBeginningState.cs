using UnityEngine;

public class GameBeginningState : ATimerState
{
    public GameBeginningState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine, duration)
    {
        nextState = EStateName.RoundsLoop;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // Send message to players with their role
    }

    public override void OnTimerUpdated(float deltaTime) { }
    public override void OnExit() { }
    public override void OnUpdate(float deltaTime) { }
    public override void Reset() { }
}