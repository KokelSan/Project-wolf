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
public struct AttributionStrategy
{
    public int PlayersNb;
    public List<CharacterDistribution> CharacterDistributions; 
}

[Serializable]
public struct CharacterDistribution
{
    public CharacterSO Character;
    public int MaxNb;
}