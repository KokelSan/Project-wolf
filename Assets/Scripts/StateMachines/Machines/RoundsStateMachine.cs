using System.Collections.Generic;
using System.Linq;

public class RoundsStateMachine : AStateMachineState
{
    public RoundsStateMachine(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName)
        : base(stateName, stateMachine, defaultNextStateName)
    {
    }

    public override void InitializeMachine()
    {
        if (!IsInitialized)
        {
            StartingStateName = EStateName.RoundBeginning;
            DefaultNextStateName = EStateName.GameEnding;

            SetState(new RoundBeginningState(StartingStateName, this, EStateName.IndividualSkills_SM));
            SetState(new IndividualSkillsStateMachine(EStateName.IndividualSkills_SM, this, EStateName.GroupSkills_SM));
            // inter skills state ?
            SetState(new GroupSkillsStateMachine(EStateName.GroupSkills_SM, this, EStateName.GeneralVote));
            // skills revelation state ?
            SetState(new GeneralVoteState(EStateName.GeneralVote, this, EStateName.RoundEnding));
            SetState(new RoundEndingState(EStateName.RoundEnding, this, EStateName.None));
        }       
    }

    protected override void OnMachineCompleted()
    {
        if (AlivePlayersWin())
        {
            Exit();
            return;
        }

        EnterState(EStateName.RoundBeginning);
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
