using System.Collections.Generic;
using System.Linq;

public class RoundsState_SM : AStateMachineState
{       
    public RoundsState_SM(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine()
    {
        StartingStateName = EStateName.RoundBeginning;
        DefaultNextStateName = EStateName.GameEnding;

        SetState(new GenericTimerState(StartingStateName, this, 1, EStateName.IndividualSkills_SM));
        SetState(new IndividualSkillsState_SM(EStateName.IndividualSkills_SM, this));
        // inter skills state ?
        SetState(new GroupSkillsState_SM(EStateName.GroupSkills_SM, this));
        // General vote
        SetState(new GenericTimerState(EStateName.RoundEnding, this, 1, EStateName.None));        
    }    

    protected override void OnMachineCompleted()
    {
        if (AlivePlayersAreWinning())
        {
            Exit();
            return;
        }

        RestartMachine();
    }    

    private bool AlivePlayersAreWinning()
    {
        // Alive players are winning if they all belong to the same winning group, i.e there is only one group represented by alive players

        List<Player> alivePlayers = GameManager.Instance?.AlivePlayers;
        List<CharactersList> winningGroups = GameManager.Instance?.GameControl?.WinningGroups?.Groups;

        int standingGroupCount = 0;

        foreach (CharactersList charactersList in winningGroups)
        {
            List<string> membersName = charactersList.Members.Select(member => member.Name).ToList();

            if (alivePlayers.Exists(player => membersName.Contains(player.CharacterInstance.Name)))
            {
                standingGroupCount++;
                continue;
            }
        }

        return standingGroupCount == 1;
    }
    
}
