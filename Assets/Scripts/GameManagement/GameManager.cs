using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool LogGameSteps = true;

    [SerializeField] private GameControlSO _gameControl;
    [SerializeField] private List<Player> _players;

    private List<ASkillSO> ResolutionOrder => _gameControl.ResolutionOrder;
    private List<AttributionStrategy> AttributionStrategies => _gameControl.AttributionStrategies;

    #region Members Management

    public void SetGameControl(GameControlSO gameControl)
    {
        _gameControl = gameControl;
    }

    public void SetPlayers(List<Player> players)
    {
        _players = players;
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
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
            Debug.LogError($"Game preparation error : {e.Message} \n\nStacktrace : \n{e.StackTrace}\n");
        }
    }

    public AttributionStrategy ComputeAttributionStrategy()
    {
        AttributionStrategy strategy = AttributionStrategies.FirstOrDefault(strat => strat.PlayersNb >= _players.Count);

        if (strategy == null) 
        {
            throw new Exception($"No strategy found for {_players.Count} players");
        }

        if (LogGameSteps)
        {
            string log = $"--- Strategy found for {_players.Count} players ---\n";
            strategy.CharacterDistributions.ForEach(distribution => log += $"  {distribution.Character.Name} : {distribution.MaxNb}\n");
            Debug.Log(log);
        }
        
        return strategy;
    }

    public void AssignCharactersToPlayers(AttributionStrategy strategy)
    {
        string log = "--- Character assignation ---";

        List<CharacterSO> availableCharacters = strategy.GetAllAvailableCharacters();
        foreach (Player player in _players) 
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);

            log += $"\n  {player.Name} : {player.CharacterInstance.name}";
        }

        if (availableCharacters.Any())
        {
            log += $"Remaining characters : ";
            availableCharacters.ForEach(character => log += $"{character.Name}, ");
        }

        if (LogGameSteps)
        {
            Debug.Log($"{log}\n");
        }
    }

    public void InitializeCharacters()
    {

    }
}