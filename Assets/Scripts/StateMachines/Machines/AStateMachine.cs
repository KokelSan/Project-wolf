using System;
using System.Collections.Generic;

/// <summary>
/// Base class to derive from to create a state machine
/// </summary>
public abstract class AStateMachine : Loggable, IStateMachine
{
    public override string LogIdentifier => $"[MACHINE {GetType()}]";

    #region IStateMachine implementations

    public Dictionary<EStateName, IState> States { get; protected set; } = new Dictionary<EStateName, IState>() { { EStateName.None, null } };
    public EStateName StartingStateName { get; protected set; }
    public EStateName CurrentStateName { get; protected set; } = EStateName.None;
    public IState CurrentState => States[CurrentStateName];
    public bool IsInitialized { get; protected set; }
    public Action OnMachineStoppedAction { get; protected set; }    

    public abstract void InitializeMachine();

    public virtual void StartMachine(Action onMachineCompleted)
    {
        Log("Starting");

        if (!IsInitialized)
        {
            InitializeMachine();
            IsInitialized = true;
        }

        OnMachineStoppedAction = onMachineCompleted;
        EnterState(StartingStateName);

        //Log("Started");
    }

    public virtual void UpdateMachine(float deltaTime)
    {        
        CurrentState?.Update(deltaTime);
    }

    public virtual void StopMachine()
    {
        Log("Stopping");

        OnMachineStoppedAction?.Invoke();

        //Log("Stopped");
    }

    public void SetState<T>(T state) where T : IState
    {
        States[state.StateName] = state;
    }

    public void EnterState(EStateName stateName)
    {       
        if (stateName.Equals(EStateName.None))
        {
            LogWarning("Trying to enter the 'None' state");
            return;
        }

        if (!States.ContainsKey(stateName))
        {            
            LogWarning($"Trying to enter the unregistered state '{stateName}'");
            return;
        }

        Log($"Entering '{stateName}'");

        CurrentStateName = stateName;
        CurrentState.Enter();

        //Log($"'{stateName}' entered");
    }

    public void ExitState(EStateName stateToExit, EStateName nextState)
    {
        Log($"Exiting '{stateToExit}' for '{nextState}'");

        if (!stateToExit.Equals(CurrentStateName))
        {
            LogWarning($"Trying to exit state '{stateToExit}' but it is not the current one");
            return;
        }

        if (!States.ContainsKey(stateToExit))
        {
            LogWarning($"Trying to exit the unregistered state '{stateToExit}'");
            return;
        }        

        if (nextState.Equals(EStateName.None))
        {
            StopMachine();
            return;
        }      

        if (nextState.Equals(stateToExit))
        {
            TryReEnterState();
            return;
        }

        EnterState(nextState);
    }

    #endregion

    public virtual void TryReEnterState() { }

}