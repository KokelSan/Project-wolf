
public interface IState
{
    /// <summary>
    /// The key registering this state in its state machine
    /// </summary>
    EStateName StateName { get; }

    /// <summary>
    /// The state machine this state belongs to
    /// </summary>
    IStateMachine StateMachine { get; }

    /// <summary>
    /// True if the state is the current state of its state machine, False otherwise
    /// </summary>
    bool IsCurrentState { get; }

    /// <summary>
    /// Called by the state machine to enter this state
    /// </summary>
    void Enter();

    /// <summary>
    /// Called at the beginning of <see cref="Enter()"/>
    /// </summary>
    void OnEnter();

    /// <summary>
    /// Updates the state on a time basis
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void Update(float deltaTime);

    /// <summary>
    /// Called at the beginning of <see cref="Update(float)"/>
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void OnUpdate(float deltaTime);

    /// <summary>
    /// Called to reset the state before re-entering it
    /// </summary>
    void Reset();

    /// <summary>
    /// Notify the state machine to exit this state, and optionnaly enter a next one
    /// </summary>
    /// <param name="nextState">The next state to enter in</param>
    void Exit(EStateName nextState);

    /// <summary>
    /// Called by <see cref="Exit(EStateName)"/> before notifying the state machine
    /// </summary>
    void OnExit();
}
