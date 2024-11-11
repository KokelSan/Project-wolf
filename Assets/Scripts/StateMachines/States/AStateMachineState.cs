/// <summary>
/// Base class for implementing a state holding its own state machine
/// </summary>
public abstract class AStateMachineState : AStateMachine, IState
{
    public override string LogTag => $"[SM-STATE {GetType()}]";

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

    public virtual void Exit()
    {
        IsCurrentState = false;
        StateMachine.ExitState(StateName, DefaultNextStateName);
    }

    #endregion

    public AStateMachineState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName)
    {
        StateName = stateName;
        StateMachine = stateMachine;
        DefaultNextStateName = defaultNextStateName;
    }

    protected virtual void OnMachineCompleted()
    {
        Exit();
    }
}
