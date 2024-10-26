using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Characters/New Character", fileName = "_Character")]
public class CharacterSO : InstantiableSO
{
    public string Name;
    [TextArea(2, 10)] public string Description;

    [Space] public List<ASkillSO> IndividualSkills;
    [Space] public List<ASkillSO> GroupSkills;

    public bool IsAlive { get; private set; }

    private CharacterSO parent;

    protected override void Initialize()
    {
        if (!TryGetParentAs(out parent))
            throw new Exception($"Impossible to cast ParentSO to CharacterSO for instance {name} ({InstanceId})");     

        IndividualSkills = InstantiableSOFactory.CreateInstances(parent.IndividualSkills);
        GroupSkills.Clear();
    }

    public List<ASkillSO> GetParentGroupSkills()
    {
        return parent?.GroupSkills ?? new List<ASkillSO>();
    }

    public void AddGroupSkill(ASkillSO skill)
    {
        if (GroupSkills == null) GroupSkills = new List<ASkillSO>();
        GroupSkills.Add(skill);
    }

    public void Kill()
    {
        IsAlive = false;
    }

    public void Revive()
    {
        IsAlive = true;
    }
}
