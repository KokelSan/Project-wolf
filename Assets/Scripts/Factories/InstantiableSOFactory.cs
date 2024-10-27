using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class InstantiableSOFactory
{
    private static readonly Dictionary<string, int> instancesSuffixes = new Dictionary<string, int>();

    /// <summary>
    /// Creates a single instance owned by multiple objects
    /// </summary>
    public static T CreateInstance<T, U>(T originalSO, List<U> ownersSO) where T : InstantiableSO where U : InstantiableSO
    {
        T instance = Object.Instantiate(originalSO);
        instance.name = ComputeInstanceName(originalSO.name);
        instance.InitializeInstance(originalSO, ownersSO.Select(owner => owner as IInstantiableSO).ToList());
        return instance;
    }

    /// <summary>
    /// Creates a single instance owned by a single object
    /// </summary>
    public static T CreateInstance<T, U>(T originalSO, U ownerSO) where T : InstantiableSO where U : InstantiableSO
    {
        List<U> owners = new List<U>();
        if (ownerSO != null) owners.Add(ownerSO);
        return CreateInstance(originalSO, owners);
    }

    /// <summary>
    /// Creates a single instance owned by no object
    /// </summary>
    public static T CreateInstance<T>(T originalSO) where T : InstantiableSO
    {
        return CreateInstance(originalSO, (InstantiableSO)null);
    }

    /// <summary>
    /// Creates a list of instances owned by a single object
    /// </summary>
    public static List<T> CreateInstances<T, U>(List<T> originalsSO, U ownerSO) where T : InstantiableSO where U : InstantiableSO
    {
        List<U> owners = new List<U>();
        if (ownerSO != null) owners.Add(ownerSO);
        List<T> instances = new List<T>();

        foreach (T originalSO in originalsSO)
        {
            instances.Add(CreateInstance(originalSO, owners));
        }
        return instances;
    }

    private static string ComputeInstanceName(string name)
    {
        if (instancesSuffixes.ContainsKey(name))
        {
            return $"{name}_{++instancesSuffixes[name]}";
        }
        else
        {
            instancesSuffixes[name] = 0;
            return $"{name}_0";
        }
    }

    public static void CleanDictionnary()
    {
        instancesSuffixes.Clear();
    }
}
