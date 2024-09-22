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

    protected bool TryGetParentAs<T>(out T parent) where T : InstantiableSO
    {
        parent = ParentSO as T;
        return parent != null;
    }
}
