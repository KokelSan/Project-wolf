using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Revive Skill", fileName = "Revive_TargetNb_Frequency")]
public class ReviveSkill_Individual : AIndividualSkill
{
    protected override void PerformOnTarget(Character target)
    {
        target.Revive();
    }
}
