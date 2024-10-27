using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class InstantiableSO : ScriptableObject, IInstantiableSO
{
    #region Interface implementation

    public int InstanceId => GetInstanceID();

    public IInstantiableSO OriginalSO { get; private set; } = null;
    public List<IInstantiableSO> OwnersSO { get; private set; } = null;
    public IInstantiableSO OwnerSO => OwnersSO.FirstOrDefault();

    public void InitializeInstance(IInstantiableSO originalSO, List<IInstantiableSO> ownerSO)
    {
        OriginalSO = originalSO;
        OwnersSO = ownerSO;
        Initialize();
    } 

    #endregion

    protected abstract void Initialize();

    protected bool TryGetParentAs<T>(out T parent) where T : InstantiableSO
    {
        parent = OriginalSO as T;
        return parent != null;
    }
}
