using System;
using System.Collections.Generic;

[Serializable]
public class DistributionStrategy
{
    public int PlayersNb;
    public List<CharacterDistribution> CharacterDistributions;

    public List<CharacterSO> AllCharacters
    {
        get
        {
            List<CharacterSO> allCharacters = new();
            foreach (CharacterDistribution distrib in CharacterDistributions)
            {
                for (int i = 0; i < distrib.MaxNb; i++)
                {
                    allCharacters.Add(distrib.Character);
                }
            }
            return allCharacters;
        }        
    }

    public bool IsValid => PlayersNb == AllCharacters.Count;
    public bool IsValidForPlayersCount(int playerCount) => PlayersNb >= playerCount && IsValid;
    
}

[Serializable]
public class CharacterDistribution
{
    public CharacterSO Character;
    public int MaxNb = 1;
}