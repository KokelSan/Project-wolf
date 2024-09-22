using UnityEngine;

public abstract class InstantiableSO : ScriptableObject, IInstantiableSO
{
    public IInstantiableSO ParentSO { get; private set; } = null;

    public int InstanceId => GetInstanceID();

    public void Initialize(IInstantiableSO parentSO)
    {
        ParentSO = parentSO;
        Initialize();
    }

    protected abstract void Initialize();
}
