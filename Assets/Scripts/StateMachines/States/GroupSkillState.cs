using System.Collections.Generic;
using System.Linq;
using System.Text;

public class GroupSkillState : ATimerState
{
    public override string LogTag => $"[GROUP SKILL]";
    public override LogColor TagColor => LogColor.cyan;

    private ASkillSO skill;

    public GroupSkillState(EStateName stateName, IStateMachine stateMachine, EStateName defaultNextStateName, float? duration = null)
        : base(stateName, stateMachine, defaultNextStateName, duration)
    {
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