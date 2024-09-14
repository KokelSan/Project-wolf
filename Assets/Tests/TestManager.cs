using System;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{   
    void Start()
    {
        PrepareGame();
    }

    public void PrepareGame()
    {
        GameManager gameManager = FindFirstObjectByType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogWarning("TEST: No GameManager found in the current scene. Test aborted.");
            return;
        }

        Player playerA = new Player(new Guid(), "Player A");
        Player playerB = new Player(new Guid(), "Player B");
        Player playerC = new Player(new Guid(), "Player C");
        gameManager.SetPlayers(new List<Player> { playerA, playerB, playerC });

        gameManager.PrepareGame();
    }    
}
