using System.Collections.Generic;
using UnityEngine;

public abstract class ASkill : ScriptableObject
{
    public string Name;
    public string Description;

    public abstract void Perform(ref List<Character> targets);
}
