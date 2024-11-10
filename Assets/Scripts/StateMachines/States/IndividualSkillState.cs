public class IndividualSkillState : ATimerState
{
    public override string LogIdentifier => $"[INDIVIDUAL SKILLS]";

    private Player player;

    public IndividualSkillState(EStateName stateName, IStateMachine stateMachine, float? duration = null) : base(stateName, stateMachine, duration)
    {
        DefaultNextStateName = stateName;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    public override void Enter()
    {
        Log($"{player.CharacterInstance.name} ({player.Name})");
                
        timer = 0.0f;
        IsCurrentState = true;
    }

    public override void Exit(EStateName? nextState = null)
    {
        IsCurrentState = false;
        StateMachine.ExitState(StateName, nextState ?? DefaultNextStateName);
    }
}