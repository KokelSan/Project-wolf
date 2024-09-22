using System.Collections.Generic;
using UnityEngine;

public static class ScriptableObjectFactory
{
    private static readonly Dictionary<string, int> instancesSuffixes = new Dictionary<string, int>();

    public static T CreateInstance<T>(T scriptableObject) where T : InstantiableSO
    {
        T instance = Object.Instantiate(scriptableObject);
        instance.name = ComputeInstanceName(scriptableObject.name);
        instance.Initialize(scriptableObject);
        return instance;
    }

    public static List<T> CreateInstances<T>(List<T> scriptableObjectList) where T : InstantiableSO
    {
        List<T> instances = new List<T>();
        foreach (T scriptableObject in scriptableObjectList)
        {
            instances.Add(CreateInstance(scriptableObject));
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
