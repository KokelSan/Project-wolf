using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameControlSO GameControl { get; private set; }
    public List<Player> Players { get; private set; }

    // Game preparation
    public DistributionStrategy CurrentStrategy { get; private set; }
    public List<Player> OrderedPlayers { get; private set; }
    public List<ASkillSO> InstantiatedGroupSkills { get; private set; }

    //Game resolution
    public GameResolutionStateMachine StateMachine { get; private set; }

    #region Members Management

    public void SetGameControl(GameControlSO gameControl)
    {
        GameControl = gameControl;
    }

    public void SetPlayers(List<Player> players)
    {
        Players = players;
    }

    public void AddPlayer(Player player)
    {
        Players.Add(player);
    }  

    public void RemovePlayer(Player player)
    {
        Players.Remove(player);
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
            throw;
        }
    }

    private void CheckGameRequirements()
    {
        if (GameControl == null) throw new Exception($"There is no game control");
        if (GameControl.ResolutionOrder?.Characters?.Any() != true && GameControl.ResolutionOrder?.GroupSkills?.Any() != true) throw new Exception($"The resolution order is null or empty");
        if (GameControl.DistributionStrategies?.Any() != true) throw new Exception($"The distribution strategies are null or empty");

        if (Players?.Any() != true) throw new Exception($"The players list is null or empty");
        if (GameControl.DistributionStrategies.OrderBy(strat => strat.PlayersNb).First()?.PlayersNb > Players.Count) throw new Exception($"There are not enough players to play");
    }    

    private DistributionStrategy ComputeDistributionStrategy()
    {
        CurrentStrategy = GameControl.DistributionStrategies.OrderBy(strat => strat.PlayersNb).FirstOrDefault(strat => strat.IsValidForPlayersCount(Players.Count));

        if (CurrentStrategy == null)
        {
            throw new Exception($"No valid strategy found for {Players.Count} players");
        }

        return CurrentStrategy;
    }

    private void CreateAndAssignCharactersToPlayers()
    {
        List<CharacterSO> availableCharacters = CharacterFactory.InstantiateFromDistributionStrategy(CurrentStrategy, Players.Count, out List<ASkillSO> instantiatedGroupSkills);
        InstantiatedGroupSkills = instantiatedGroupSkills;

        foreach (Player player in Players)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);
        }

        GenerateOrderedPlayersList();
    }

    private void GenerateOrderedPlayersList()
    {
        OrderedPlayers = new List<Player>();
        foreach (CharacterSO character in GameControl.ResolutionOrder.Characters)
        {
            OrderedPlayers.AddRange(Players.Where(player => player.CharacterInstance.Name.Equals(character.Name)).ToList());
        }
    }

    #endregion

    #region Game Resolution

    public void ResolveGame()
    {
        try
        {
            StateMachine = new GameResolutionStateMachine(this);
            StateMachine.StartMachine(OnGameResolutionStateMachineCompleted);

            foreach (Player player in OrderedPlayers) 
            {
                
            }

            foreach (ASkillSO skill in InstantiatedGroupSkills)
            {

            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Game resolution aborted : {e.Message} \n\n{e.ToString()}\n");
            throw;
        }
    }

    private void Update()
    {
        StateMachine?.UpdateMachine(Time.deltaTime);
    }

    public void OnGameResolutionStateMachineCompleted()
    {
        Debug.Log($"Game resolution state machine completed !");
    }

    private List<CharacterSO> GetCharactersWithGroupSkill(ASkillSO skill)
    {
        List<CharacterSO> characters = new List<CharacterSO>();

        var charactersWithSkill = Players.Where(player => player.CharacterInstance.GroupSkills.Contains(skill)).ToList();

        return characters;
    }

    #endregion

}