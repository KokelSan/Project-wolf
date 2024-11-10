/// <summary>
/// Base class for implementing a state holding its own state machine
/// </summary>
public abstract class AStateMachineState : AStateMachine, IState
{
    public override string LogIdentifier => $"[SM-STATE {GetType()}]";

    #region State implementations

    public EStateName StateName { get; protected set; }
    public IStateMachine StateMachine { get; protected set; }
    public bool IsCurrentState { get; protected set; }
    public EStateName DefaultNextStateName { get; protected set; } = EStateName.None;

    public virtual void Enter()
    {
        IsCurrentState = true;
        StartMachine(OnMachineCompleted);
    }

    public virtual void Update(float deltaTime) 
    {
        if (!IsCurrentState) return;

        UpdateMachine(deltaTime);
    }

    public virtual void Exit(EStateName? nextState = null)
    {
        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState ?? DefaultNextStateName);
    }

    #endregion

    public AStateMachineState(EStateName stateName, IStateMachine stateMachine)
    {
        StateName = stateName;
        StateMachine = stateMachine;
    }

    protected virtual void OnMachineCompleted()
    {
        Exit(DefaultNextStateName);
    }
}
