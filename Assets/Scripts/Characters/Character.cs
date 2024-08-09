using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Character", fileName = "_CharacterSO")]
public class Character : ScriptableObject
{
    public string Name;
    public string Description;

    public List<ASkill> Skills;
}