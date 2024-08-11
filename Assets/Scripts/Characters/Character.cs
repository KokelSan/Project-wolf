using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Character", fileName = "_Character")]
public class Character : ScriptableObject
{
    public string Name;
    public string Description;

    public List<AIndividualSkill> IndividualSkills;
    public List<GroupSkill> GroupSkills;

    public bool IsAlive { get; private set; } = true;

    public void Kill()
    {
        IsAlive = false;
    }

    public void Revive()
    {
        IsAlive = true;
    }
}
