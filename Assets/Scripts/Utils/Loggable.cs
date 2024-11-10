using UnityEngine;

public abstract class Loggable
{
    public abstract string LogIdentifier { get; }

    public void Log(string message, bool withIdentifier = true)
    {
        Debug.Log($"{(withIdentifier ? $"{LogIdentifier} " : "")}{message}\n\n");
    }

    public void LogWarning(string message, bool withIdentifier = true)
    {
        Debug.LogWarning($"{(withIdentifier ? $"{LogIdentifier} " : "")}{message}\n\n");
    }

    public void LogError(string message, bool withIdentifier = true)
    {
        Debug.LogError($"{(withIdentifier ? $"{LogIdentifier} " : "")}{message}\n\n");
    }
}