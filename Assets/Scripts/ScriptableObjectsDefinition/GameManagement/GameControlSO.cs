using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Management/New Game Control", fileName = "_GameControl")]
public class GameControlSO : ScriptableObject
{
    public List<ASkillSO> ResolutionOrder;
    public List<DistributionStrategy> DistributionStrategies;
}
