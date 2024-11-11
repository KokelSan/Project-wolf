using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GeneralVoteState : ATimerState
{
    public override string LogIdentifier => $"[GENERAL VOTE]";
    public override LogColor LogIdentifierColor => LogColor.orange;

    private List<Player> AlivePlayers => GameManager.Instance?.AlivePlayers;

    public GeneralVoteState(EStateName stateName, IStateMachine stateMachine, float? duration = null) : base(stateName, stateMachine, duration) { }

    public override void Exit(EStateName? nextState = null)
    {
        int playerIndex = Random.Range(0, AlivePlayers.Count);
        Player playerToKill = AlivePlayers[playerIndex];
        GameManager.Instance?.KillPlayer(playerToKill);

        StringBuilder sb = new StringBuilder().AppendLine($"{playerToKill.Name} killed ({playerToKill.CharacterInstance.name})").AppendLine($"Alive players:");
        AlivePlayers.ForEach((player) => sb.AppendLine($"  {player.CharacterInstance.name}"));
        Log(sb.ToString());

        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState ?? DefaultNextStateName);
    }
}