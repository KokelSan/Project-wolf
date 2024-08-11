using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Resolution Order/New Resolution Order", fileName = "_ResolutionOrder")]
public class ResolutionOrder : ScriptableObject
{
    public List<Character> OrderedCharacters;
}
