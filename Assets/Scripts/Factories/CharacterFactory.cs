using System.Collections.Generic;
using UnityEngine;

public static class CharacterFactory 
{  
    public static List<CharacterSO> InstantiateFromDistributionStrategy(DistributionStrategy strategy, int playersCount, out string unassignedLog, bool randomizeResultList = true)
    {        
        // All characters available from the distribution, possibly with more elements than the players count
        List<CharacterSO> availableCharacters = new(strategy.AllCharacters);

        // So we have to remove elements randomly to match the players count
        int nbToRemove = availableCharacters.Count - playersCount;
        unassignedLog = nbToRemove > 0 ? "Unassigned : " : string.Empty;
        for (int i = 0; i < nbToRemove; i++)
        {
            int index = Random.Range(0, availableCharacters.Count);
            unassignedLog += $"{availableCharacters[index].Name} {(i == nbToRemove - 1 ? "" : ", ")}";
            availableCharacters.RemoveAt(index);
        }

        // Then the characters are instantiated, randomly or not
        List<CharacterSO> characters = new List<CharacterSO>();
        for (int i = 0; i < playersCount; i++)
        {
            int index = randomizeResultList ? Random.Range(0, availableCharacters.Count) : i;
            CharacterSO character = availableCharacters[index];

            CharacterSO instance = ScriptableObjectFactory<CharacterSO>.CreateInstance(character);
            instance.name = $"{character.name}_{i}";
            characters.Add(instance);

            if (randomizeResultList) availableCharacters.RemoveAt(index);
        }
        return characters;
    }    
}
