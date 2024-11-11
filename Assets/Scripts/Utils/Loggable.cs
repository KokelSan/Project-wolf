using UnityEngine;

public abstract class Loggable
{
    public abstract string LogIdentifier { get; }
    public virtual LogColor LogIdentifierColor { get; } = LogColor.white;

    public void Log(string message, bool withIdentifier = true, LogColor? messageColor = null)
    {
        string identifier = withIdentifier ? $"{ApplyColor(LogIdentifier, LogIdentifierColor)} " : "";
        Debug.Log($"{identifier}{ApplyColor(message, messageColor)}");
    }

    private string ApplyColor(string message, LogColor? color = null)
    {
        if (color == null) return message;

        return $"<color={color}>{message}</color>";
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning(message);
    }

    public void LogError(string message)
    {
        Debug.LogError(message);
    }    
}

public enum LogColor
{
    black,
    blue,
    brown,
    cyan,
    darkblue,
    green,
    grey,
    lightblue,
    lime,
    magenta,
    maroon,
    navy,
    olive,
    orange,
    purple,
    red,
    silver,
    teal,
    white,
    yellow
}