using EditorAttributes;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [PropertyDropdown] public GameControlSO GameControlSO;
    public int PlayersNb;

    [HideInInspector] public List<Player> Players;
}
