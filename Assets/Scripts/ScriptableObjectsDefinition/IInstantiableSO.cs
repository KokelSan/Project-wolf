using UnityEngine;

public interface IInstantiableSO
{
    ScriptableObject ParentSO { get; }

    /// <summary>
    /// Useful for debug as it appears when serializing the object in Json for example
    /// </summary>
    int InstanceId { get; }

    void Initialize(ScriptableObject parentSO);
}
