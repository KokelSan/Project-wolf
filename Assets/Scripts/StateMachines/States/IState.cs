/// <summary>
/// Base interface for implementing a state of a state machine
/// </summary>
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
    /// True if the state is the current state of its owning state machine, False otherwise
    /// </summary>
    bool IsCurrentState { get; }

    /// <summary>
    /// The default next state of the machine after exiting this one
    /// </summary>
    EStateName DefaultNextStateName { get; }

    /// <summary>
    /// Called by the state machine to enter this state
    /// </summary>
    void Enter();

    /// <summary>
    /// Updates the state on a time basis
    /// </summary>
    /// <param name="deltaTime">Elapsed time between the previous frame</param>
    void Update(float deltaTime);

    /// <summary>
    /// Notify the state machine to exit this state, and optionnaly enter a next one. <br/>
    /// If <paramref name="nextState"/> is null, the next state will be <see cref="DefaultNextStateName"/>
    /// </summary>
    /// <param name="nextState">The next state to enter in</param>
    void Exit(EStateName? nextState);
}
