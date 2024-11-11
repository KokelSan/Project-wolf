using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    // Game preparation
    public GameControlSO GameControl { get; private set; }
    public List<Player> Players { get; private set; }

    // Game resolution
    public DistributionStrategy CurrentStrategy { get; private set; }
    public List<Player> AlivePlayers { get; private set; }
    public List<Player> OrderedPlayersWithIndividualSkills { get; private set; }
    public List<List<Player>> PlayersWinningGroups { get; private set; }
    public List<ASkillSO> OrderedGroupSkills { get; private set; }
    public GameStateMachine StateMachine { get; private set; }

    // End game
    public List<Player> WinningPlayers { get; private set; }

    #region Members Management

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

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

    public void KillPlayer(Player player)
    {
        player.CharacterInstance.Kill();

        AlivePlayers.Remove(player);
        OrderedPlayersWithIndividualSkills.Remove(player);        

        foreach (ASkillSO groupSkill in player.CharacterInstance.GroupSkills)
        {
            List<CharacterSO> members = groupSkill.OwnersSO.Select(member => member as CharacterSO).ToList();
            if (members.All(member => !member.IsAlive))
            {
                OrderedGroupSkills.Remove(groupSkill);
            }
        }
    }

    public void SetWinners(List<Player> winners)
    {
        WinningPlayers = winners;
    }

    #endregion

    #region Game Preparation

    public void PrepareGame()
    {
        try
        {
            CheckGameRequirements();

            ComputeDistributionStrategy();
            CreateCharacters();
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
        if (GameControl.DistributionStrategies?.Strategies?.Any() != true) throw new Exception($"The distribution strategies are null or empty");

        if (Players?.Any() != true) throw new Exception($"The players list is null or empty");
        if (GameControl.DistributionStrategies.Strategies?.OrderBy(strat => strat.PlayersNb).First()?.PlayersNb > Players.Count) throw new Exception($"There are not enough players to play");
    }    

    private DistributionStrategy ComputeDistributionStrategy()
    {
        CurrentStrategy = GameControl.DistributionStrategies.Strategies?.OrderBy(strat => strat.PlayersNb).FirstOrDefault(strat => strat.IsValidForPlayersCount(Players.Count));

        if (CurrentStrategy == null)
        {
            throw new Exception($"No valid strategy found for {Players.Count} players");
        }

        return CurrentStrategy;
    }

    private void CreateCharacters()
    {
        List<CharacterSO> availableCharacters = CharacterFactory.InstantiateFromDistributionStrategy(CurrentStrategy, Players.Count, out List<ASkillSO> instantiatedGroupSkills);
        OrderedGroupSkills = instantiatedGroupSkills;

        // Players' characters are affected randomly.
        // Depending on the strategy found, it might be more available characters than actual players.
        foreach (Player player in Players)
        {
            int randomIndex = UnityEngine.Random.Range(0, availableCharacters.Count);
            player.SetCharacterInstance(availableCharacters[randomIndex]);
            availableCharacters.RemoveAt(randomIndex);
        }

        OrderedPlayersWithIndividualSkills = new List<Player>();
        foreach (CharacterSO character in GameControl.ResolutionOrder.Characters)
        {
            OrderedPlayersWithIndividualSkills.AddRange(Players.Where(player => player.CharacterInstance.Name.Equals(character.Name)).ToList());
        }

        AlivePlayers = new List<Player>(Players);
        PlayersWinningGroups = new List<List<Player>>();

        foreach (CharactersList charactersList in GameControl.WinningGroups.Groups)
        {
            List<Player> playersInGroup = Players.Join(charactersList.Members, player => player.CharacterInstance.Name, member => member.Name, (player, member) => player).ToList();
            PlayersWinningGroups.Add(playersInGroup);
        }
    }

    #endregion

    #region Game Resolution

    public void StartGame(Action onGameCompletedOverride = null)
    {
        try
        {
            StateMachine = new GameStateMachine();
            StateMachine.StartMachine(onGameCompletedOverride ?? OnGameCompleted);
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

    public void OnGameCompleted()
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