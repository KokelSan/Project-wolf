using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Kill Skill", fileName = "Kill_TargetNb_Frequency")]
public class KillSkillSO : ASkillSO
{
    protected override void Execute(CharacterSO target)
    {
        target.Kill();
    }
}
