/// <summary>
/// Base class to derive from to create a state of a state machine
/// </summary>
public abstract class AState : Loggable, IState
{
    public override string LogTag => $"[STATE {StateName}]";

    #region IState implementations

    public EStateName StateName { get; protected set; }
    public IStateMachine StateMachine { get; protected set; }
    public bool IsCurrentState { get; protected set; }
    public EStateName DefaultNextStateName { get; protected set; }

    public AState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName)
    {
        StateName = stateName;
        StateMachine = stateMachine;
        DefaultNextStateName = defaultNextStateName;
    }

    public virtual void Enter()
    {
        IsCurrentState = true;
    }

    public virtual void Update(float deltaTime) { }

    public virtual void Exit()
    {
        IsCurrentState = false;
        StateMachine.ExitState(StateName, DefaultNextStateName);
    }

    #endregion      
}