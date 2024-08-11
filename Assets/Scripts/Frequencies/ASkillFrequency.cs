using UnityEngine;

public abstract class ASkillFrequency : ScriptableObject
{
    public abstract bool CanPerform();
    public abstract void Update();
}
