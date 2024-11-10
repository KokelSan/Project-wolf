/// <summary>
/// Base class to derive from to create a state of a state machine
/// </summary>
public abstract class AState : Loggable, IState
{
    public override string LogIdentifier => $"[STATE {StateName}]";

    #region IState implementations

    public EStateName StateName { get; protected set; }
    public IStateMachine StateMachine { get; protected set; }
    public bool IsCurrentState { get; protected set; }
    public EStateName DefaultNextStateName { get; protected set; } = EStateName.None;

    public AState(EStateName stateName, IStateMachine stateMachine)
    {
        StateName = stateName;
        StateMachine = stateMachine;
    }

    public virtual void Enter()
    {
        IsCurrentState = true;
    }

    public virtual void Update(float deltaTime) { }

    public virtual void Exit(EStateName? nextState = null)
    {
        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState ?? DefaultNextStateName);
    }

    #endregion      
}