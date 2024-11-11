public class RoundEndingState : ATimerState
{
    public RoundEndingState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName, float? duration = null)
        : base(stateName, stateMachine, defaultNextStateName, duration)
    {
    }

    public override void Enter()
    {
        GameManager.Instance.ResolvePendingActions();

        base.Enter();
    }
}
