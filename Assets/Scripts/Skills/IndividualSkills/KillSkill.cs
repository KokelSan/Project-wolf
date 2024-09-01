using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Kill Skill", fileName = "Kill_TargetNb_Frequency")]
public class KillSkill : ASkill
{
    protected override void Execute(Character target)
    {
        target.Kill();
    }
}
