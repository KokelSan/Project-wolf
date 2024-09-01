using UnityEngine;

public abstract class ASkillFrequency : ScriptableObject
{
    public abstract bool CanExecute();
    public abstract void Update();
}
