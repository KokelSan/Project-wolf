using System.Collections.Generic;
using System.Linq;

public class RoundsStateMachine : AStateMachineState
{       
    public RoundsStateMachine(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine()
    {
        if (!IsInitialized)
        {
            StartingStateName = EStateName.RoundBeginning;
            DefaultNextStateName = EStateName.GameEnding;

            SetState(new GenericTimerState(StartingStateName, this, EStateName.IndividualSkills_SM));
            SetState(new IndividualSkillsStateMachine(EStateName.IndividualSkills_SM, this));
            // inter skills state ?
            SetState(new GroupSkillsStateMachine(EStateName.GroupSkills_SM, this));
            SetState(new GeneralVoteState(EStateName.GeneralVote, this));
            SetState(new GenericTimerState(EStateName.RoundEnding, this, EStateName.None));
        }       
    }

    protected override void OnMachineCompleted()
    {
        if (AlivePlayersWin())
        {
            Exit();
            return;
        }

        Log("--- New round ---", withIdentifier: false);

        if (GameManager.Instance.OrderedPlayersWithIndividualSkills.Any())
        {
            EnterState(EStateName.IndividualSkills_SM);
            return;
        }

        if (GameManager.Instance.OrderedGroupSkills.Any())
        {
            EnterState(EStateName.GroupSkills_SM);
            return;
        }

        EnterState(EStateName.GeneralVote);
    }

    private bool AlivePlayersWin()
    {
        // Alive players are winning if they all belong to the same winning group, i.e there is only one group represented by alive players
        List<List<Player>> standingGroups = new List<List<Player>>();

        foreach (List<Player> playersList in GameManager.Instance.PlayersWinningGroups)
        {
            if (playersList.Any(player => player.IsAlive))
            {
                standingGroups.Add(playersList);
            }            
        }

        if (standingGroups.Count == 1)
        {
            GameManager.Instance.SetWinners(standingGroups.FirstOrDefault());
            return true;
        }

        return false;
    }    
}
