using System;
using System.Collections.Generic;

/// <summary>
/// Base interface for implementing a state machine
/// </summary>
public interface IStateMachine
{
    /// <summary>
    /// Dictionnary of all the states composing the state machine
    /// </summary>
    Dictionary<EStateName, IState> States { get; }

    /// <summary>
    /// The name of the state to enter when the machine is started
    /// </summary>
    EStateName StartingStateName { get; }

    /// <summary>
    /// The name of the current state of the machine
    /// </summary>
    EStateName CurrentStateName { get; }

    /// <summary>
    /// The current state of the machine. Retrieved with <see cref="CurrentStateName"/>
    /// </summary>
    IState CurrentState { get; }

    /// <summary>
    /// True if the machine has already been initialized, false otherwise
    /// </summary>
    bool IsInitialized { get; }

    /// <summary>
    /// Action to perform when the machine exits
    /// </summary>
    Action OnMachineStoppedAction { get; }

    /// <summary>
    /// Called at the beginning of <see cref="StartMachine(Action)"/> before entering the starting state. Useful to initializes the machine by instantiating and adding its states or refresh some data in case of restart
    /// </summary>
    void InitializeMachine();

    /// <summary>
    /// Starts the machine and enters its first state
    /// </summary>
    /// <param name="onMachineCompleted">Action to perform when the machine exits its last state</param>
    void StartMachine(Action onMachineCompleted);

    /// <summary>
    /// Updates the machine and its current state on a time basis
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void UpdateMachine(float deltaTime);

    /// <summary>
    /// Terminates the machine and trigger the <see cref="OnMachineStoppedAction"/> action
    /// </summary>
    void StopMachine();

    /// <summary>
    /// Adds or set a state in the<see cref="States"/> dictionnary
    /// </summary>
    /// <param name="state">The state to register</param>
    /// </summary>
    void SetState<T>(T state) where T : IState;

    /// <summary>
    /// Enters the given state if found in the <see cref="States"/> dictionnary
    /// </summary>
    /// <param name="stateName">The state to enter</param>
    void EnterState(EStateName stateName);

    /// <summary>
    /// Exits the given state and optionnaly enters the next one
    /// </summary>
    /// <param name="stateToExit">The current state to exit</param>
    /// <param name="nextState">
    /// The next state to enter. If equals : <br/>
    /// - <see cref="EStateName.None"/> : the machine will be exited <br/>
    /// - <paramref name="stateToExit"/> : the state will be reseted (see <see cref="TryReEnterCurrentState"/>) before getting entered
    /// </param>
    void ExitState(EStateName stateToExit, EStateName nextState = EStateName.None);

    /// <summary>
    /// Called before re-entering the current state
    /// </summary>
    void TryReEnterCurrentState();
}
