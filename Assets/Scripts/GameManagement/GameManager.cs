using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool LogGameSteps = true;

    [SerializeField] private GameControlSO _gameControl;
    [SerializeField] private List<Player> _players;

    private List<ASkillSO> ResolutionOrder => _gameControl.ResolutionOrder;
    private List<DistributionStrategy> DistributionStrategies => _gameControl.DistributionStrategies;

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

    private void CheckGameRequirements()
    {
        if (_gameControl == null) throw new Exception($"There is no game control");
        if (ResolutionOrder == null) throw new Exception($"There is no resolution order");
        if (DistributionStrategies == null) throw new Exception($"There is no distribution strategies");

        if (_players?.Any() != true) throw new Exception($"The players list is empty or null");
        if (DistributionStrategies?.First()?.PlayersNb > _players.Count) throw new Exception($"There are not enough players to play");
    }

    public void PrepareGame()
    {    
        try
        {
            CheckGameRequirements();

            DistributionStrategy strategy = ComputeDistributionStrategy();
            AssignCharactersToPlayers(strategy);
        }
        catch (Exception e)
        {
            Debug.LogError($"Game aborted : {e.Message} \n\n{e.ToString()}\n");
        }
    }    

    private DistributionStrategy ComputeDistributionStrategy()
    {
        DistributionStrategy strategy = DistributionStrategies.FirstOrDefault(strat => strat.IsValidForPlayersCount(_players.Count));

        if (strategy == null) 
        {
            throw new Exception($"No valid strategy found for {_players.Count} players");
        }

        if (LogGameSteps)
        {
            string log = $"--- Strategy found for {_players.Count} players ---\n";
            strategy.CharacterDistributions.ForEach(distribution => log += $"  {distribution.Character.Name} : {distribution.MaxNb}\n");
            Debug.Log(log);
        }
        
        return strategy;
    }

    private void AssignCharactersToPlayers(DistributionStrategy strategy)
    {
        string log = "--- Character assignation ---";

        List<CharacterSO> availableCharacters = CharacterFactory.InstantiateFromDistributionStrategy(strategy, _players.Count, out string unassignedLog);
        foreach (Player player in _players) 
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);

            log += $"\n  {player.Name} : {player.CharacterInstance.Name} ({player.CharacterInstance.name})";
        }

        if (!string.IsNullOrEmpty(unassignedLog))
        {
            log += $"\n  {unassignedLog}";
        }

        if (LogGameSteps)
        {
            Debug.Log($"{log}\n");
        }
    }

    private void InitializeCharacters()
    {

    }
}