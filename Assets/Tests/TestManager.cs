using EditorAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    [PropertyDropdown] public GameControlSO GameControlSO;
    public int PlayersNb;

    private List<Player> players;

    
    [Button("Test Game Preparation")]
    public void PrepareGame()
    {
        ScriptableObjectFactory.CleanDictionnary();

        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.SetGameControl(GameControlSO);
        gameManager.SetPlayers(CreatePlayers());
        gameManager.PrepareGame();

        DestroyImmediate(gameManager);
    }

    private List<Player> CreatePlayers()
    {
        players = new List<Player>();
        for (int i = 1; i <= PlayersNb; i++)
        {
            players.Add(new Player(new Guid(), $"Player {i}"));
        }
        return players;
    }

    [Button("Debug Players")]
    public void DebugPlayers()
    {
        if (players?.Any() != true) return;

        foreach (Player player in players)
        {
            Debug.Log($"{JsonConvert.SerializeObject(player, Formatting.Indented)}\n\n");
        }
    }

    [Button("Clear Players")]
    public void ClearPlayers()
    {
        players?.Clear();
    }
}
