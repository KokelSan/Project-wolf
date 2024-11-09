using UnityEngine;

public abstract class AState : IState
{
    #region Interface implementations

    public EStateName StateName { get; protected set; }

    public IStateMachine StateMachine { get; protected set; }

    public bool IsCurrentState { get; protected set; }

    public virtual void Enter()
    {
        Debug.Log($"Entering state '{GetType()}'");

        OnEnter();
        IsCurrentState = true;
    }

    public abstract void OnEnter();

    public virtual void Update(float deltaTime)
    {
        if (!IsCurrentState) return;

        OnUpdate(deltaTime);
    }

    public abstract void OnUpdate(float deltaTime);

    public abstract void Reset();

    public virtual void Exit(EStateName nextState)
    {
        Debug.Log($"Exiting state '{GetType()}'");

        OnExit();
        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState);        
    }

    public abstract void OnExit();

    #endregion

    public AState(EStateName stateName, IStateMachine stateMachine)
    {
        StateName = stateName;
        StateMachine = stateMachine;
    }
}