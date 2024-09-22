using EditorAttributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TestLauncher : MonoBehaviour
{
    public TestManager TestManager => GetComponent<TestManager>();

    [Button("Test Game Preparation")]
    public void PrepareGame()
    {
        InstantiableSOFactory.CleanDictionnary();

        GameManager gameManager = gameObject.AddComponent<GameManager>();
        gameManager.SetGameControl(TestManager.GameControlSO);
        gameManager.SetPlayers(CreatePlayers());
        gameManager.PrepareGame();

        DestroyImmediate(gameManager);
    }

    private List<Player> CreatePlayers()
    {
        TestManager.Players = new List<Player>();
        for (int i = 1; i <= TestManager.PlayersNb; i++)
        {
            TestManager.Players.Add(new Player(new Guid(), $"Player {i}"));
        }
        return TestManager.Players;
    }

    [Button("Debug Players")]
    public void DebugPlayers()
    {
        if (TestManager.Players?.Any() != true) return;

        foreach (Player player in TestManager.Players)
        {


            StringBuilder sb = new StringBuilder().AppendLine($"{player.Name.ToUpper()}");
            sb.AppendLine($"{player.CharacterInstance.name} (parent = {player.CharacterInstance})");


            Debug.Log($"{sb.ToString()}\n\n");

        }
    }

    [Button("Clear Players")]
    public void ClearPlayers()
    {
        TestManager.Players?.Clear();
    }
}
