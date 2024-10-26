
public interface IInstantiableSO
{
    IInstantiableSO ParentSO { get; }

    /// <summary>
    /// Useful for debug as it appears when serializing the object in Json for example
    /// </summary>
    int InstanceId { get; }

    void InitializeFromParent(IInstantiableSO parentSO);
}
