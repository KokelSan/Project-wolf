/// <summary>
/// Base class to derive from to create a state based on a limited duration. <br/>
/// The state will automatically exit after the given duration.
/// </summary>
public abstract class ATimerState : AState
{
    protected float duration = 3f;
    protected float timer = 0f;

    public ATimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine)
    {
        this.duration = duration;
    }

    public override void Enter()
    {       
        timer = 0f;
        base.Enter();
    }

    public override void Update(float deltaTime) 
    {
        if (!IsCurrentState) return;

        timer += deltaTime;

        if (timer >= duration)
        {
            Exit();
            return;
        }

        OnTimerUpdated(deltaTime);
    }

    public virtual void OnTimerUpdated(float deltaTime) { }
}

public class GenericTimerState : ATimerState
{
    public GenericTimerState(EStateName stateName, IStateMachine stateMachine, float duration, EStateName nextState) : base(stateName, stateMachine, duration)
    {
        DefaultNextStateName = nextState;
    }
}
