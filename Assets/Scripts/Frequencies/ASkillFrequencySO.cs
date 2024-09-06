using UnityEngine;

public abstract class ASkillFrequencySO : ScriptableObject
{
    public abstract bool CanExecute();
    public abstract void Update();
}
