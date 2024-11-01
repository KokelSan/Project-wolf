using System;
using System.Collections.Generic;

public interface IStateMachine
{
    Dictionary<EStateName, IState> States { get; }
    EStateName CurrentStateKey { get; }
    IState CurrentState { get; }
    Action OnMachineCompleted { get; }

    void InitializeMachine();
    void StartMachine(Action onMachineCompleted);    
    void UpdateMachine(float deltaTime);
    void ExitMachine();

    void AddState<T>(T state) where T : IState;
    void EnterState(EStateName stateName);
    void ExitState(EStateName stateToExit, EStateName nextState = EStateName.None);    
}
