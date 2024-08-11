using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skills/New Group Skill", fileName = "Name_TargetNb_Frequency_GroupName")]

public class GroupSkill : ASkill
{
    public AIndividualSkill RelatedSkill;
    public bool AllCharactersAreMembers = false;
    public List<Character> GroupMembers;

    public override void Perform(ref List<Character> targets)
    {
        RelatedSkill.Perform(ref targets);
    }
}
