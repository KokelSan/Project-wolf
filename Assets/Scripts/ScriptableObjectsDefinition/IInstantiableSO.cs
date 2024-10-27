using System.Collections.Generic;

public interface IInstantiableSO
{
    int InstanceId { get; }

    IInstantiableSO OriginalSO { get; }
    List<IInstantiableSO> OwnersSO { get; }
    IInstantiableSO OwnerSO { get; }

    void InitializeInstance(IInstantiableSO originalSO, List<IInstantiableSO> ownerSO);
}
