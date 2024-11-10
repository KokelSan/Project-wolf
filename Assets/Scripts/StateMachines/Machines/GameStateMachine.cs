public class GameStateMachine : AStateMachine
{
    public override void InitializeMachine()
    {
        StartingStateName = EStateName.GameBeginning;

        SetState(new GenericTimerState(StartingStateName, this, 1, EStateName.Rounds_SM));
        SetState(new RoundsState_SM(EStateName.Rounds_SM, this));
        SetState(new GenericTimerState(EStateName.GameEnding, this, 1, EStateName.None));
    }
}