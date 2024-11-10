using System.Collections.Generic;

public class IndividualSkillsStateMachine : AStateMachineState
{
    private List<Player> PlayersWithIndividualSkills => GameManager.Instance?.OrderedPlayersWithIndividualSkills;
    private Player CurrentPlayer => PlayersWithIndividualSkills[currentPlayerIndex];
    private int currentPlayerIndex;
    private IndividualSkillState individualSkillState;

    public IndividualSkillsStateMachine(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine() 
    {
        if (!IsInitialized)
        {
            StartingStateName = EStateName.Skill;
            DefaultNextStateName = EStateName.GroupSkills_SM;

            individualSkillState = new IndividualSkillState(StartingStateName, this);
            SetState(individualSkillState);   

            IsInitialized = true;
        }

        currentPlayerIndex = 0;
        individualSkillState.SetPlayer(CurrentPlayer);
    }

    public override void TryReEnterCurrentState()
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
