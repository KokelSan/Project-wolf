using UnityEngine;

public abstract class Loggable
{
    public abstract string LogTag { get; }
    public virtual LogColor TagColor { get; } = LogColor.white;

    /// <summary>
    /// Logs the given message
    /// </summary>
    /// <param name="message">The message to log</param>
    /// <param name="withTag">True to add the colorized Tag before the message, false otherwise</param>
    /// <param name="messageColor">An optionnal color for the message (does not affect the tag)</param>
    public void Log(string message, bool withTag = true, LogColor? messageColor = null)
    {
        string tag = withTag ? $"{ApplyColor(LogTag, TagColor)} " : "";

        if (string.IsNullOrWhiteSpace(message))
        {
            Debug.Log($"{tag}\n\n");
            return;
        }

        if (messageColor == null)
        {
            Debug.Log($"{tag}{message}\n\n");
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

        Debug.Log($"{tag}{ApplyColor(firstLineContent, messageColor)}{restOfContent}\n\n");
    }

    private string ApplyColor(string message, LogColor? color)
    {
        if (color == null) return message;

        return $"<color={color}>{message}</color>";
    }

    public void LogWarning(string message)
    {
        Debug.LogWarning($"{LogTag} {message}");
    }

    public void LogError(string message)
    {
        Debug.LogError($"{LogTag} {message}");
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