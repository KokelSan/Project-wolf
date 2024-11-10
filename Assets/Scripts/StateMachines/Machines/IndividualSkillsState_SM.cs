using System.Collections.Generic;

public class IndividualSkillsState_SM : AStateMachineState
{
    private List<Player> PlayersWithIndividualSkills => GameManager.Instance?.PlayersWithIndividualSkills;
    private Player CurrentPlayer => PlayersWithIndividualSkills[currentPlayerIndex];
    private int currentPlayerIndex = 0;
    private IndividualSkillState individualSkillState;

    public IndividualSkillsState_SM(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine() 
    {
        StartingStateName = EStateName.IndividualSkill;
        DefaultNextStateName = EStateName.GroupSkills_SM;

        individualSkillState = new IndividualSkillState(StartingStateName, this, 1, CurrentPlayer);
        SetState(individualSkillState);       
    }

    public override void TryReEnterState()
    {
        currentPlayerIndex++;
        if (currentPlayerIndex >= PlayersWithIndividualSkills.Count)
        {
            Exit();
            return;
        }

        individualSkillState?.SetPlayer(CurrentPlayer);
        EnterState(CurrentStateName);
    }
}

public class IndividualSkillState : ATimerState
{
    private Player player;

    public IndividualSkillState(EStateName stateName, IStateMachine stateMachine, float duration, Player player) : base(stateName, stateMachine, duration)
    {
        DefaultNextStateName = stateName;
        this.player = player;
    }

    public void SetPlayer(Player player)
    {
        this.player = player;
    }
}