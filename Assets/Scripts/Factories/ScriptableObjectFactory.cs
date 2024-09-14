using UnityEngine;

public static class ScriptableObjectFactory<T> where T : ScriptableObject
{  
    public static T CreateInstance(T scriptableObject) 
    {
        T instance = ScriptableObject.Instantiate<T>(scriptableObject);
        //LogInstanceCreation(instance, scriptableObject);
        return instance;
    }

    private static void LogInstanceCreation(T instance, T original)
    {
        Debug.Log(
            $"Instance '{instance.name}' of type '{instance.GetType()}' created." +
            $"\n  Instance Id = {instance.GetInstanceID()}" +
            $"\n  Original Id = {original.GetInstanceID()}\n");
    }
}
