using System.Linq;

public class RoundBeginningState : ATimerState
{
    private int roundCount = 0;

    public RoundBeginningState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName, float? duration = null)
        : base(stateName, stateMachine, defaultNextStateName, duration) 
    { 
    }

    public override void Enter()
    {
        roundCount++;
        Log($"[ROUND {roundCount}]", withTag:false, LogColor.white);

        GameManager.Instance.ResetPendingActions();

        base.Enter();
    }

    public override void Exit()
    {
        EStateName nextState;

        if (GameManager.Instance.OrderedPlayersWithIndividualSkills.Any())
        {
            nextState = EStateName.IndividualSkills_SM;
        }
        else if (GameManager.Instance.OrderedGroupSkills.Any())
        {
            nextState = EStateName.GroupSkills_SM;
        }
        else
        {
            nextState = EStateName.GeneralVote;
        }

        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState);
    }
}
