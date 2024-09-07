using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Management/New Game Control", fileName = "_GameControl")]
public class GameControlSO : ScriptableObject
{
    public List<CharacterSO> ResolutionOrder;
    public List<AttributionStrategy> AttributionStrategies;
}

[Serializable]
public class AttributionStrategy
{
    public int PlayersNb;
    public List<CharacterDistribution> CharacterDistributions;

    public List<CharacterSO> GetAllAvailableCharacters()
    {
        List<CharacterSO> characters = new List<CharacterSO> ();
        foreach (CharacterDistribution distrib in CharacterDistributions)
        {
            string type = distrib.Character.GetType().ToString();
            CharacterSO instance = ScriptableObject.CreateInstance(type) as CharacterSO;

            if (instance == null)
            {
                Debug.LogWarning($"Impossible to cast from {type} to CharacterSO for SO '{distrib.Character.name}'");
                continue;
            }

            characters.Add(instance);
        }
        return characters;
    }

}

[Serializable]
public class CharacterDistribution
{
    public CharacterSO Character;
    public int MaxNb;
}