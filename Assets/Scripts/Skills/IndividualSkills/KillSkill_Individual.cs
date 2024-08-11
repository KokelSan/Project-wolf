using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Kill Skill", fileName = "Kill_TargetNb_Frequency")]
public class KillSkill_Individual : AIndividualSkill
{
    protected override void PerformOnTarget(Character target)
    {
        target.Kill();
    }
}
