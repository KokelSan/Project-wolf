using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Management/New Game Control", fileName = "_GameControl")]
public class GameControlSO : ScriptableObject
{
    public ResolutionOrder ResolutionOrder;
    public List<DistributionStrategy> DistributionStrategies;
}

[Serializable]
public class ResolutionOrder
{
    public List<CharacterSO> Characters;
    public List<ASkillSO> GroupSkills;
}