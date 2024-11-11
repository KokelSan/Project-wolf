using EditorAttributes;
using System;
using UnityEngine;

public class ColorTester : MonoBehaviour
{
    [Button]
    public void Test()
    {       
        foreach (LogColor color in Enum.GetValues(typeof(LogColor)))
        {
            Debug.Log($"<color={color}>This is an example text for color '{color}'</color>");
        }
    }
}