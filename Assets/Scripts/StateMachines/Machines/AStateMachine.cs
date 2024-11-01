using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class AStateMachine : IStateMachine
{   
    #region Interface implementations

    public Dictionary<EStateName, IState> States { get; protected set; } = new Dictionary<EStateName, IState>();
    public EStateName CurrentStateKey { get; protected set; } = EStateName.None;
    public IState CurrentState => States[CurrentStateKey];
    public Action OnMachineCompleted { get; protected set; }


    public abstract void InitializeMachine();

    public virtual void StartMachine(Action onMachineCompleted)
    {
        OnMachineCompleted = onMachineCompleted;
    }

    public void UpdateMachine(float deltaTime)
    {
        CurrentState?.Update(deltaTime);
    }

    public void ExitMachine()
    {
        OnMachineCompleted?.Invoke();
    }

    public void AddState<T>(T state) where T : IState
    {
        if (States.ContainsKey(state.StateName))
        {
            Debug.LogWarning($"State '{state.StateName.ToString()}' has already been registered as a state of the machine");
            return;
        }

        States.Add(state.StateName, state);
    }

    public void EnterState(EStateName stateName)
    {
        if (States.ContainsKey(stateName))
        {
            CurrentStateKey = stateName;
            CurrentState.Enter();
            Debug.Log($"State '{stateName.ToString()}' entered");
            return;
        }

        Debug.LogWarning($"State '{stateName.ToString()}' is not registered as a state of the machine");
    }

    public void ExitState(EStateName stateToExit, EStateName nextState = EStateName.None)
    {
        Debug.Log($"State '{stateToExit.ToString()}' exited");

        if (nextState.Equals(EStateName.None))
        {
            ExitMachine();
            return;
        }            

        EnterState(nextState);
    }

    #endregion

    public AStateMachine()
    {
        InitializeMachine();
    }   
}