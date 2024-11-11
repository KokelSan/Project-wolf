using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class GeneralVoteState : ATimerState
{    
    public override string LogTag => $"[GENERAL VOTE]";
    public override LogColor TagColor => LogColor.red;

    private List<Player> AlivePlayers => GameManager.Instance?.AlivePlayers;

    public GeneralVoteState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName, float? duration = null)
        : base(stateName, stateMachine, defaultNextStateName, duration)
    {
    }

    public override void Exit()
    {
        int playerIndex = Random.Range(0, AlivePlayers.Count);
        Player playerToKill = AlivePlayers[playerIndex];
        GameManager.Instance?.KillPlayer(playerToKill);

        StringBuilder sb = new StringBuilder().AppendLine($"{playerToKill.Name} killed ({playerToKill.CharacterInstance.name})").AppendLine($"Alive players:");
        AlivePlayers.ForEach((player) => sb.AppendLine($"  {player.CharacterInstance.name}"));
        Log(sb.ToString(), messageColor:TagColor);

        base.Exit();
    }
}