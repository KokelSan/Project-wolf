using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class CharacterFactory 
{  
    public static List<CharacterSO> InstantiateFromDistributionStrategy(DistributionStrategy strategy, int playersCount)
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

        CreateGroupSkills(characters);

        return characters;
    }

    private static void CreateGroupSkills(List<CharacterSO> characters)
    {
        Dictionary<ASkillSO, ASkillSO> instantiatedSkill = new Dictionary<ASkillSO, ASkillSO>();

        foreach (CharacterSO character in characters) 
        {
            foreach (ASkillSO skill in character.GetParentGroupSkills())
            {
                if (!instantiatedSkill.ContainsKey(skill))
                {
                    instantiatedSkill.Add(skill, InstantiableSOFactory.CreateInstance(skill));
                }

                character.AddGroupSkill(instantiatedSkill[skill]);
            }
        }        
    }
}
