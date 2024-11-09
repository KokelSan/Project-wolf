using System;
using System.Collections.Generic;

public interface IStateMachine
{
    /// <summary>
    /// Dictionnary of all the states composing the state machine
    /// </summary>
    Dictionary<EStateName, IState> States { get; }

    /// <summary>
    /// The key of the current state the machine is in
    /// </summary>
    EStateName CurrentStateKey { get; }

    /// <summary>
    /// The state the machine is in. Retrieved with <see cref="CurrentStateKey"/>
    /// </summary>
    IState CurrentState { get; }

    /// <summary>
    /// Action to perform when the machine exits
    /// </summary>
    Action OnMachineCompleted { get; }

    /// <summary>
    /// Initializes the machine by instantiating and adding its states
    /// </summary>
    void InitializeMachine();

    /// <summary>
    /// Starts the machine and enters its first state
    /// </summary>
    /// <param name="onMachineCompleted">Action to perform when the machine exits its last state</param>
    void StartMachine(Action onMachineCompleted);

    /// <summary>
    /// Called at the beginning of <see cref="StartMachine(Action)"/>
    /// </summary>
    void OnStart();

    /// <summary>
    /// Updates the machine on a time basis
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void UpdateMachine(float deltaTime);

    /// <summary>
    /// Called at the beginning of <see cref="UpdateMachine(float)"/>
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void OnUpdate(float deltaTime);

    /// <summary>
    /// Terminates the machine and trigger the <see cref="OnMachineCompleted"/> action
    /// </summary>
    void ExitMachine();

    /// <summary>
    /// Called at the beginning of <see cref="ExitMachine()"/>
    /// </summary>
    void OnExit();

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
    /// - <paramref name="stateToExit"/> : the state will be reseted (see <see cref="ResetCurrentState"/>) before getting entered
    /// </param>
    void ExitState(EStateName stateToExit, EStateName nextState = EStateName.None);

    /// <summary>
    /// Called before re-entering the current state
    /// </summary>
    void ResetCurrentState();
}
