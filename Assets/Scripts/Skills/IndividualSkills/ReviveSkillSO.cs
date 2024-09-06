using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Revive Skill", fileName = "Revive_TargetNb_Frequency")]
public class ReviveSkillSO : ASkillSO
{
    protected override void Execute(CharacterSO target)
    {
        target.Revive();
    }
}
