using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroupSkillState : ATimerState
{
    public override string LogIdentifier => $"[GROUP SKILL]";
    public override LogColor LogIdentifierColor => LogColor.cyan;

    private ASkillSO skill;

    public GroupSkillState(EStateName stateName, IStateMachine stateMachine, float? duration = null) : base(stateName, stateMachine, duration)
    {
        DefaultNextStateName = stateName;
    }

    public void SetSkill(ASkillSO skill)
    {
        this.skill = skill;
    }

    public override void Enter()
    {
        List<CharacterSO> owners = skill.OwnersSO.Select(owner => owner as CharacterSO).ToList();
        StringBuilder sb = new StringBuilder().AppendLine($"{skill.name}").AppendLine($"Members:");
        owners.ForEach((owner) => sb.AppendLine($"  {owner.name}{(owner.IsAlive ? "" : " (dead)")}"));
        Log(sb.ToString());

        base.Enter();
    }
}