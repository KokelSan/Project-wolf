
public abstract class AState : IState
{
    #region Interface implementations

    public EStateName StateName { get; protected set; }

    public IStateMachine StateMachine { get; protected set; }

    public bool IsCurrentState { get; protected set; }

    public void Enter()
    {
        OnEnter();
        IsCurrentState = true;
    }

    public void Exit(EStateName nextState = EStateName.None)
    {
        OnExit();
        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState);
    }

    public virtual void OnEnter() { }
    public virtual void Update(float deltaTime) { }
    public virtual void OnExit() { }

    #endregion

    public AState(EStateName stateName, IStateMachine stateMachine)
    {
        StateName = stateName;
        StateMachine = stateMachine;
    }
}