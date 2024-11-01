using EditorAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(TestManager))]
public class TestLauncher : MonoBehaviour
{
    private TestManager testManager => GetComponent<TestManager>();

    private bool gamePrepared = false;

    #region Game Preparation

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, true, "Test Game Preparation")]
    public void PrepareGame()
    {
        try
        {
            testManager.GameManager = CreateGameManager();
            testManager.GameManager.SetGameControl(testManager.GameControlSO);
            testManager.GameManager.SetPlayers(CreatePlayers());

            testManager.GameManager.PrepareGame();
            DebugGamePreparation();

            gamePrepared = true;
        }
        catch {}
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
            testManager.Players.Add(new Player(Guid.NewGuid(), $"Player {i}"));
        }
        return testManager.Players;
    }    

    #endregion

    #region Game Resolution

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, false, "Test Game Resolution")]
    public void ResolveGame()
    {
        testManager.GameManager.ResolveGame();
        //DebugGameResolution();
    }

    #endregion

    #region Debug

    private void DebugGamePreparation()
    {
        List<Player> players = testManager.GameManager.Players;

        StringBuilder sb = new StringBuilder().AppendLine($"--- Strategy found for {players.Count} players ---");
        testManager.GameManager.CurrentStrategy.CharacterDistributions.ForEach(distribution => sb.AppendLine($"    {distribution.Character.Name} : {distribution.MaxNb}"));
        Debug.Log(sb.AppendLine().ToString());


        sb.Clear();
        sb.AppendLine("--- Character assignation ---");
        Dictionary<ASkillSO, List<CharacterSO>> groupSkillsDict = new Dictionary<ASkillSO, List<CharacterSO>>();
        foreach (Player player in players)
        {
            CharacterSO character = player.CharacterInstance;
            sb.AppendLine($"    {player.Name} is a {character.Name} ({character.name}: Id = {character.InstanceId}, parent = {character.OriginalSO.InstanceId})");

            foreach (ASkillSO skill in character.IndividualSkills)
            {
                sb.Append($"         {skill.name} (Id = {skill.InstanceId}, parent = {skill.OriginalSO.InstanceId})");
                sb.AppendLine($" @ {skill.Frequency.name} (Id = {skill.Frequency.InstanceId}, parent = {skill.Frequency.OriginalSO.InstanceId})");
            }

            foreach (ASkillSO skill in character.GroupSkills)
            {
                sb.Append($"         {skill.name} (Id = {skill.InstanceId}, parent = {skill.OriginalSO.InstanceId})");
                sb.AppendLine($" @ {skill.Frequency.name} (Id = {skill.Frequency.InstanceId}, parent = {skill.Frequency.OriginalSO.InstanceId})");
            }

            sb.AppendLine();
        }

        Debug.Log(sb.ToString());

        sb.Clear();
        sb.AppendLine($"--- Resolution order ---");
        testManager.GameManager.OrderedPlayers.ForEach(player => sb.AppendLine($"    {player.Name} ({player.CharacterInstance.Name})"));
        sb.AppendLine();
        testManager.GameManager.InstantiatedGroupSkills.ForEach(skill => sb.AppendLine($"    {skill.name}"));
        Debug.Log(sb.AppendLine().ToString());
                
    }

    private void DebugGameResolution()
    {
        StringBuilder sb = new StringBuilder().AppendLine($"--- Game resolution ---").AppendLine();


       
        Debug.Log(sb.AppendLine().ToString());
    }

    #endregion

    [Button(nameof(gamePrepared), ConditionResult.EnableDisable, false, "Reset")]
    public void ResetTest()
    {
        transform.GetComponentsInChildren<GameManager>().ToList().ForEach(gm => DestroyImmediate(gm.gameObject));
        InstantiableSOFactory.CleanDictionnary();
        testManager.Players?.Clear();
        gamePrepared = false;
    } 
}
