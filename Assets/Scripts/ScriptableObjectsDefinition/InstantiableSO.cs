using UnityEngine;

public abstract class InstantiableSO : ScriptableObject, IInstantiableSO
{
    public ScriptableObject ParentSO { get; private set; } = null;

    public int InstanceId => GetInstanceID();

    public void Initialize(ScriptableObject parentSO)
    {
        ParentSO = parentSO;
        Initialize();
    }

    protected abstract void Initialize();
}
