using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameControlSO _gameControl;
    [SerializeField] private List<Player> _players;

    public List<Player> Players => _players;

    private List<ASkillSO> ResolutionOrder => _gameControl.ResolutionOrder;
    private List<DistributionStrategy> DistributionStrategies => _gameControl.DistributionStrategies;

    public DistributionStrategy CurrentStrategy { get; private set; }

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

    #region Game Preparation

    public void PrepareGame()
    {
        try
        {
            CheckGameRequirements();

            ComputeDistributionStrategy();
            CreateAndAssignCharactersToPlayers();
        }
        catch (Exception e)
        {
            Debug.LogError($"Game preparation aborted : {e.Message} \n\n{e.ToString()}\n");
        }
    }

    private void CheckGameRequirements()
    {
        if (_gameControl == null) throw new Exception($"There is no game control");
        if (ResolutionOrder?.Any() != true) throw new Exception($"The resolution order is null or empty");
        if (DistributionStrategies?.Any() != true) throw new Exception($"The distribution strategies are null or empty");

        if (_players?.Any() != true) throw new Exception($"The players list is null or empty");
        if (DistributionStrategies.OrderBy(strat => strat.PlayersNb).First()?.PlayersNb > _players.Count) throw new Exception($"There are not enough players to play");
    }    

    private DistributionStrategy ComputeDistributionStrategy()
    {
        CurrentStrategy = DistributionStrategies.OrderBy(strat => strat.PlayersNb).FirstOrDefault(strat => strat.IsValidForPlayersCount(_players.Count));

        if (CurrentStrategy == null)
        {
            throw new Exception($"No valid strategy found for {_players.Count} players");
        }

        return CurrentStrategy;
    }

    private void CreateAndAssignCharactersToPlayers()
    {
        List<CharacterSO> availableCharacters = CharacterFactory.InstantiateFromDistributionStrategy(CurrentStrategy, _players.Count);
        foreach (Player player in _players)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);
        }
    }

    #endregion

    #region Game Resolution

    public void ResolveGame()
    {
        try
        {
            
        }
        catch (Exception e)
        {
            Debug.LogError($"Game resolution aborted : {e.Message} \n\n{e.ToString()}\n");
        }
    }

    #endregion

}