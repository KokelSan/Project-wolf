using System.Collections.Generic;
using System.Linq;

public class RoundsLoopState : AState
{       
    private RoundStateMachine turnStateMachine;

    public RoundsLoopState(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void OnEnter()
    {
        StartNewTurn();
    }

    public override void Update(float deltaTime)
    {
        turnStateMachine?.UpdateMachine(deltaTime);
    }

    public override void OnExit() { }

    private void StartNewTurn()
    {
        turnStateMachine = new RoundStateMachine();
        turnStateMachine.StartMachine(OnTurnCompleted);
    }

    private void OnTurnCompleted()
    {
        if (AlivePlayersAreWinning())
        {
            Exit(EStateName.GameEnding);
        }
        else
        {
            StartNewTurn();
        }        
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

    public override void OnUpdate(float deltaTime) { }
    public override void Reset() { }
}
