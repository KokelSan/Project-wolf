public class GameStateMachine : AStateMachine
{
    public override void InitializeMachine()
    {        
        if (!IsInitialized)
        {
            StartingStateName = EStateName.GameBeginning;

            SetState(new GenericTimerState(StartingStateName, this, EStateName.Rounds_SM));
            SetState(new RoundsStateMachine(EStateName.Rounds_SM, this, EStateName.GameEnding));
            SetState(new GenericTimerState(EStateName.GameEnding, this, EStateName.None));

            IsInitialized = true;
        }
    }
}