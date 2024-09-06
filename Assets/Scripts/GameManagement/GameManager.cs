using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
    [SerializeField] private GameControlSO gameControl;
    [SerializeField] private List<Player> players;

    private List<CharacterSO> ResolutionOrder => gameControl.ResolutionOrder;
    private List<AttributionStrategy> AttributionStrategies => gameControl.AttributionStrategies;

    #region Members Management

    private void SetGameControl(GameControlSO _gameControl)
    {
        gameControl = _gameControl;
    }

    private void SetPlayers(List<Player> _players)
    {
        players = _players;
    }

    private void AddPlayer(Player player)
    {
        players.Add(player);
    }

    private void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    #endregion

    private void PrepareGame()
    {

    }

    private void AssignCharactersToPlayers()
    {

    }

    private void InitializeCharacters()
    {

    }
}