using UnityEngine;

public abstract class Loggable
{
    public abstract string LogIdentifier { get; }

    public void Log(string message)
    {
        Debug.Log($"{LogIdentifier} {message}");
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning($"{LogIdentifier} {message}");
    }

    public void LogError(string message)
    {
        Debug.LogError($"{LogIdentifier} {message}");
    }
}