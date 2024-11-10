using System.Collections.Generic;

public class GroupSkillsStateMachine : AStateMachineState
{
    private List<ASkillSO> GroupSkills => GameManager.Instance?.OrderedGroupSkills;
    private ASkillSO CurrentSkill => GroupSkills[currentSkillIndex];
    private int currentSkillIndex;
    private GroupSkillState groupSkillState;

    public GroupSkillsStateMachine(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine()
    {
        if (!IsInitialized)
        {
            StartingStateName = EStateName.Skill;
            DefaultNextStateName = EStateName.GeneralVote;

            groupSkillState = new GroupSkillState(StartingStateName, this, 1);
            SetState(groupSkillState);

            IsInitialized = true;
        }

        currentSkillIndex = 0;
        groupSkillState.SetSkill(CurrentSkill);
    }

    public override void TryReEnterCurrentState()
    {
        currentSkillIndex++;
        if (currentSkillIndex >= GroupSkills.Count)
        {
            Exit();
            return;
        }

        groupSkillState?.SetSkill(CurrentSkill);
        EnterState(CurrentStateName);
    }
}