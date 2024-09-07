using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Management/New Game Control", fileName = "_GameControl")]
public class GameControlSO : ScriptableObject
{
    public List<ASkillSO> ResolutionOrder;
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
            
            // TODO: rework the instantiation process, must clone all the data of the original character

            if (instance == null)
            {
                Debug.LogWarning($"Impossible to cast from {type} to CharacterSO for SO '{distrib.Character.name}'");
                continue;
            }
            
            Debug.Log($"Instance of type '{type}' created. Character name = {instance.Name} (original character name = {distrib.Character.Name})");

            characters.Add(instance);
        }
        return characters;
    }
}

[Serializable]
public class CharacterDistribution
{
    public CharacterSO Character;
    public int MaxNb = 1;
}