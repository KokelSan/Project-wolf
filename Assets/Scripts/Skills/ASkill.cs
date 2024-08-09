using System.Collections.Generic;
using UnityEngine;

public abstract class ASkill : ScriptableObject
{
    public string Name;
    public string Description;

    public ASkillFrequency Frequency;
    public List<Archetype> Targets;

    public abstract void Perform();
}