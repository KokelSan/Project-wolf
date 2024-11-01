
public interface IState
{
    EStateName StateName { get; }
    IStateMachine StateMachine { get; }
    bool IsCurrentState { get; }

    void Enter();
    void OnEnter();

    void Update(float deltaTime);

    void Exit(EStateName nextState = EStateName.None);
    void OnExit();
}
