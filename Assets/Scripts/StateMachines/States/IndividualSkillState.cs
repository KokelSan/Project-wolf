public class IndividualSkillState : ATimerState
{
    public override string LogTag => $"[INDIVIDUAL SKILLS]";
    public override LogColor TagColor => LogColor.lime;

    private Player player;

    public IndividualSkillState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName, float? duration = null)
        : base(stateName, stateMachine, defaultNextStateName, duration)
    {
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Log($"{player.CharacterInstance.name} ({player.Name})");

        base.Enter();
    }

    public override void Exit()
    {
        GameManager.Instance.AddPendingAction(player, RoundActionType.Kill);

        base.Exit();
    }
}