using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStateMachine : IStateMachine
{
    #region Interface implementations

    public Dictionary<EStateName, IState> States { get; protected set; } = new Dictionary<EStateName, IState>() { { EStateName.None, null } };
    public EStateName CurrentStateKey { get; protected set; } = EStateName.None;
    public IState CurrentState => States[CurrentStateKey];
    public Action OnMachineCompleted { get; protected set; }

    public abstract void InitializeMachine();

    public void StartMachine(Action onMachineCompleted)
    {
        Debug.Log($"Starting machine '{GetType()}'");

        OnStart();
        OnMachineCompleted = onMachineCompleted;        
    }

    public abstract void OnStart();

    public void UpdateMachine(float deltaTime)
    {        
        OnUpdate(deltaTime);
        CurrentState?.Update(deltaTime);
    }

    public abstract void OnUpdate(float deltaTime);

    public void ExitMachine()
    {
        Debug.Log($"Exiting machine '{GetType()}'");

        OnExit();
        OnMachineCompleted?.Invoke();
    }

    public abstract void OnExit();

    public void SetState<T>(T state) where T : IState
    {
        States[state.StateName] = state;
    }

    public void EnterState(EStateName stateName)
    {
        if (!States.ContainsKey(stateName))
        {            
            Debug.LogWarning($"State '{stateName.ToString()}' is not registered as a state of the machine");
            return;
        }

        CurrentStateKey = stateName;
        CurrentState.Enter();
    }

    public void ExitState(EStateName stateToExit, EStateName nextState)
    {
        if (!States.ContainsKey(stateToExit))
        {
            Debug.LogWarning($"Trying to exit unregistered state '{stateToExit.ToString()}'");
            return;
        }        

        if (nextState.Equals(EStateName.None))
        {
            ExitMachine();
        } 
        else
        {
            if (nextState.Equals(stateToExit))
            {
                ResetCurrentState();
            }

            EnterState(nextState);
        }        
    }

    public virtual void ResetCurrentState()
    {
        CurrentState.Reset();
    }

    #endregion

    public AStateMachine()
    {
        InitializeMachine();
    }
}