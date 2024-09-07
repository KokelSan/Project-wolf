using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Debug = true;

    [SerializeField] private GameControlSO gameControl;
    [SerializeField] private List<Player> players;

    private List<CharacterSO> ResolutionOrder => gameControl.ResolutionOrder;
    private List<AttributionStrategy> AttributionStrategies => gameControl.AttributionStrategies;

    #region Members Management

    public void SetGameControl(GameControlSO _gameControl)
    {
        gameControl = _gameControl;
    }

    public void SetPlayers(List<Player> _players)
    {
        players = _players;
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    #endregion

    public void PrepareGame()
    {
        try
        {
            AttributionStrategy strategy = ComputeAttributionStrategy();
            AssignCharactersToPlayers(strategy);
        }
        catch (Exception e)
        {
            UnityEngine.Debug.LogError($"{e.Message}");
        }
    }

    public AttributionStrategy ComputeAttributionStrategy()
    {
        AttributionStrategy strategy = AttributionStrategies.FirstOrDefault(strat => strat.PlayersNb >= players.Count);

        if (strategy == null) 
        {
            throw new Exception($"No strategy found for {players.Count} players");
        }

        if (Debug)
        {
            UnityEngine.Debug.Log($"Strategy found for {players.Count} players : {strategy.PlayersNb} players");
        }

        return strategy;
    }

    public void AssignCharactersToPlayers(AttributionStrategy strategy)
    {
        string debug = "Character assignation :";

        List<CharacterSO> availableCharacters = strategy.GetAllAvailableCharacters();
        foreach (Player player in players) 
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);

            debug += $"\n  {player.Name} : {player.CharacterInstance.Name}";
        }

        if (availableCharacters.Any())
        {
            debug += $"Remaining characters : ";
            availableCharacters.ForEach(character => debug += $"{character.Name}, ");
        }

        if (Debug)
        {
            debug
        }
    }

    public void InitializeCharacters()
    {

    }
}