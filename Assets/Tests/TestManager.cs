using System;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{   
    public GameControlSO GameControlSO;
    public int PlayersNb;

    void Start()
    {
        PrepareGame();
    }

    public void PrepareGame()
    {
        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.SetGameControl(GameControlSO);
        gameManager.SetPlayers(CreatePlayers());
        gameManager.PrepareGame();
    }    

    private List<Player> CreatePlayers()
    {
        List<Player> players = new List<Player>();
        for (int i = 1; i <= PlayersNb; i++)
        {
            players.Add(new Player(new Guid(), $"Player {i}"));
        }
        return players;
    }
}
