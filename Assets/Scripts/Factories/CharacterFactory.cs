using System.Collections.Generic;

public static class CharacterFactory 
{  
    public static List<CharacterSO> CreateCharactersFromDistributions(List<CharacterDistribution> distributions)
    {
        List<CharacterSO> characters = new List<CharacterSO>();
        foreach (CharacterDistribution distrib in distributions)
        {
            for (int i = 0; i < distrib.MaxNb; i++)
            {
                CharacterSO instance = ScriptableObjectFactory<CharacterSO>.CreateInstance(distrib.Character);
                instance.name = $"{distrib.Character.name}_{i}";
                characters.Add(instance);
            }            
        }
        return characters;
    }    
}
