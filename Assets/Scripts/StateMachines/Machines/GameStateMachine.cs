
public class GameStateMachine : AStateMachine
{
    public override void InitializeMachine()
    {
        SetState(new GameBeginningState(EStateName.GameBeginning, this, 1));
        SetState(new RoundsLoopState(EStateName.RoundsLoop, this));
        SetState(new GameEndingState(EStateName.GameEnding, this, 1));
    }    

    public override void OnStart()
    {
        EnterState(EStateName.GameBeginning);
    }

    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }
}