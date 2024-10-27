using System.Collections.Generic;
using UnityEngine;

public static class CharacterFactory 
{  
    public static List<CharacterSO> InstantiateFromDistributionStrategy(DistributionStrategy strategy, int playersCount, out List<ASkillSO> groupSkills)
    {        
        // All characters available from the distribution, possibly with more elements than the players count
        List<CharacterSO> availableCharacters = new(strategy.AllCharacters);

        // So we have to remove elements randomly to match the players count
        int nbToRemove = availableCharacters.Count - playersCount;
        for (int i = 0; i < nbToRemove; i++)
        {
            int index = Random.Range(0, availableCharacters.Count);
            availableCharacters.RemoveAt(index);
        }

        // Then the characters are randomly instantiated
        List<CharacterSO> characters = new List<CharacterSO>();   
        
        for (int i = 0; i < playersCount; i++)
        {
            int index = Random.Range(0, availableCharacters.Count);
            CharacterSO character = availableCharacters[index];

            CharacterSO instance = InstantiableSOFactory.CreateInstance(character);
            characters.Add(instance);

            availableCharacters.RemoveAt(index);
        }

        groupSkills = CreateGroupSkills(characters);

        return characters;
    }

    private static List<ASkillSO> CreateGroupSkills(List<CharacterSO> characters)
    {
        Dictionary<ASkillSO, List<CharacterSO>> groupSkillsToInstantiate = new Dictionary<ASkillSO, List<CharacterSO>>();

        foreach (CharacterSO character in characters) 
        {
            foreach (ASkillSO skill in character.GetGroupSkillsToInstanttiate())
            {
                if (!groupSkillsToInstantiate.ContainsKey(skill))
                {
                    groupSkillsToInstantiate.Add(skill, new List<CharacterSO>());
                }

                groupSkillsToInstantiate[skill].Add(character);                
            }
        }

        List<ASkillSO> instantiatedGroupSkills = new List<ASkillSO>();
        foreach (KeyValuePair<ASkillSO, List<CharacterSO>> kvp in groupSkillsToInstantiate)
        {
            ASkillSO skill = InstantiableSOFactory.CreateInstance(kvp.Key, kvp.Value);
            instantiatedGroupSkills.Add(skill);

            foreach (CharacterSO character in kvp.Value)
            {
                character.AddGroupSkill(skill);
            }
        }

        return instantiatedGroupSkills;
    }
}
