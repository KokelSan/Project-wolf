using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Characters/New Character", fileName = "_Character")]
public class Character : ScriptableObject
{
    public string Name;
    public string Description;

    public List<ASkill> IndividualSkills;
    public List<ASkill> GroupSkills;

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
