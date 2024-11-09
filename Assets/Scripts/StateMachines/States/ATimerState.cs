
public abstract class ATimerState : AState
{
    protected float duration = 3f;
    protected float timer = 0f;
    protected EStateName nextState = EStateName.None;

    public ATimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine)
    {
        this.duration = duration;
    }

    public override void OnEnter()
    {
        timer = 0f;
    }

    public override void Update(float deltaTime) 
    {
        if (!IsCurrentState) return;

        timer += deltaTime;

        if (timer >= duration)
        {
            Exit(nextState);
        }

        OnTimerUpdated(deltaTime);
    }

    public abstract void OnTimerUpdated(float deltaTime);
}
