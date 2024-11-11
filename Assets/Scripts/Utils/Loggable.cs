using UnityEngine;

public abstract class Loggable
{
    public abstract string LogIdentifier { get; }
    public virtual LogColor LogIdentifierColor { get; } = LogColor.white;

    public void Log(string message, bool withIdentifier = true, LogColor? messageColor = null)
    {
        string identifier = withIdentifier ? $"{ApplyColor(LogIdentifier, LogIdentifierColor)} " : "";

        if (messageColor == null)
        {
            Debug.Log($"{identifier}{message}\n\n");
            return;
        }

        // The first line of the message is considered as the essential and relevant data and will be colorized, the rest of the message will appear normally
        int fistLineDelimiter = message.IndexOf("\n");
        string firstLineContent, restOfContent;
        if (fistLineDelimiter < 0)
        {
            firstLineContent = message;
            restOfContent = "";
        }
        else
        {
            firstLineContent = message.Substring(0, fistLineDelimiter);
            restOfContent = message.Substring(fistLineDelimiter);
        }

        Debug.Log($"{identifier}{ApplyColor(firstLineContent, messageColor)}{restOfContent}\n\n");
    }

    private string ApplyColor(string message, LogColor? color)
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