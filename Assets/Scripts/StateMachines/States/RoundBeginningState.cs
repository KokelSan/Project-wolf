
public class RoundBeginningState : ATimerState
{
    public RoundBeginningState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine, duration)
    {
        nextState = EStateName.SkillsLoop;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        // Send message to players
    }

    public override void OnTimerUpdated(float deltaTime) { }
    public override void OnExit() { }
    public override void OnUpdate(float deltaTime) { }
    public override void Reset() { }
}