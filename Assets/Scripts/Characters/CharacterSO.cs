using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Character", fileName = "_Character")]
public class CharacterSO : ScriptableObject
{
    public string Name;
    [TextArea(2, 10)] public string Description;

    [Space] public List<ASkillSO> IndividualSkills;
    [Space] public List<ASkillSO> GroupSkills;

    private List<ASkillSO> individualSkills_Instantiated;
    private List<ASkillSO> groupSkills_Instantiated;

    public bool IsAlive { get; private set; }

    public void Initialize(List<ASkillSO> _individualSkills_Instantiated, List<ASkillSO> _groupSkills_Instantiated)
    {
        individualSkills_Instantiated = _individualSkills_Instantiated;
        groupSkills_Instantiated = _groupSkills_Instantiated;
        IsAlive = true;
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
