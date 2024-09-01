using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Revive Skill", fileName = "Revive_TargetNb_Frequency")]
public class ReviveSkill : ASkill
{
    protected override void Execute(Character target)
    {
        target.Revive();
    }
}
