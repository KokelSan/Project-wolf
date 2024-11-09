using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Management/New Game Control", fileName = "_GameControl")]
public class GameControlSO : ScriptableObject
{
    public ResolutionOrder ResolutionOrder;
    public DistributionStrategies DistributionStrategies;
    public WinningGroups WinningGroups;
}

[Serializable]
public class ResolutionOrder
{
    public List<CharacterSO> Characters;
    public List<ASkillSO> GroupSkills;
}

[Serializable]
public class DistributionStrategies
{
    public List<DistributionStrategy> Strategies;
}

[Serializable]
public class WinningGroups
{
    public List<CharactersList> Groups;
}

[Serializable]
public class CharactersList
{
    public List<CharacterSO> Members;
}