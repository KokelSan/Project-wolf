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

    protected override void Initialize()
    {
        if (!TryGetParentAs(out CharacterSO parent))
            throw new Exception($"Impossible to cast ParentSO to CharacterSO for instance {name} ({InstanceId})");

        IndividualSkills = InstantiableSOFactory.CreateInstances(parent.IndividualSkills);
        GroupSkills = InstantiableSOFactory.CreateInstances(parent.GroupSkills);
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
