using System;
using System.Collections.Generic;

[Serializable]
public class DistributionStrategy
{
    public int PlayersNb;
    public List<CharacterDistribution> CharacterDistributions;
}

[Serializable]
public class CharacterDistribution
{
    public CharacterSO Character;
    public int MaxNb = 1;
}