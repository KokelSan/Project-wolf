using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Character", fileName = "_Character")]
public class CharacterSO : InstantiableSO
{
    public string Name;
    [TextArea(2, 10)] public string Description;

    [Space] public List<ASkillSO> IndividualSkills;
    [Space] public List<ASkillSO> GroupSkills;

    [HideInInspector] public List<ASkillSO> individualSkills_Instantiated;
    private List<ASkillSO> groupSkills_Instantiated;

    public bool IsAlive { get; private set; }

    protected override void Initialize()
    {
        individualSkills_Instantiated = InstantiableSOFactory.CreateInstances(IndividualSkills);
        groupSkills_Instantiated = InstantiableSOFactory.CreateInstances(GroupSkills);
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
