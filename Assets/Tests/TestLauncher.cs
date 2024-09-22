using EditorAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestLauncher : MonoBehaviour
{
    private TestManager testManager;
    private bool gamePrepared = false;

    #region Game Preparation

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, true, "Test Game Preparation")]
    public void PrepareGame()
    {
        if (testManager == null) testManager = GetComponent<TestManager>();

        
        testManager.GameManager = CreateGameManager();
        testManager.GameManager.SetGameControl(testManager.GameControlSO);
        testManager.GameManager.SetPlayers(CreatePlayers());
        testManager.GameManager.PrepareGame();
        
        gamePrepared = true;
    }

    private GameManager CreateGameManager()
    {
        GameObject go = new GameObject("GameManager");
        go.transform.parent = transform;
        return go.AddComponent<GameManager>();
    }

    private List<Player> CreatePlayers()
    {
        testManager.Players = new List<Player>();
        for (int i = 1; i <= testManager.PlayersNb; i++)
        {
            testManager.Players.Add(new Player(new Guid(), $"Player {i}"));
        }
        return testManager.Players;
    }

    #endregion

    #region Game Resolution

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, false, "Test Game Resolution")]
    public void ResolveGame()
    {
        testManager.GameManager.ResolveGame();
    }

    #endregion


    #region Common

    private bool allowPlayersDebug => gamePrepared && testManager.Players?.Any() == true;
    [Button(nameof(allowPlayersDebug), ConditionResult.EnableDisable, false, "Debug Players")]
    public void DebugPlayers()
    {
        if (testManager.Players?.Any() != true) return;

        foreach (Player player in testManager.Players)
        {
            Debug.Log($"{JsonConvert.SerializeObject(player, Formatting.Indented)}\n\n");
        }
    }

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, false, "Reset")]
    public void ResetTest()
    {
        DestroyImmediate(testManager.GameManager.gameObject);
        InstantiableSOFactory.CleanDictionnary();
        testManager.Players?.Clear();
        gamePrepared = false;
    }

    #endregion

}
