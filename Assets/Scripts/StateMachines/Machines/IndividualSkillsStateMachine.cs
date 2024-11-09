using System.Collections.Generic;
using UnityEngine;

public class IndividualSkillsStateMachine : AStateMachine
{
    private List<Player> PlayersWithIndividualSkills => GameManager.Instance?.PlayersWithIndividualSkills;
    private Player CurrentPlayer => PlayersWithIndividualSkills[currentPlayerIndex];
    private int currentPlayerIndex;

    public override void InitializeMachine() { }

    public override void OnStart()
    {
        StartNewSkill();
    }

    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }

    public override void ResetCurrentState()
    {
        Debug.Log($"Reset State");

        currentPlayerIndex++;
        if (currentPlayerIndex >= PlayersWithIndividualSkills.Count)
        {
            ExitMachine();
            return;
        }

        StartNewSkill();
    }

    private void StartNewSkill()
    {
        SetState(new IndividualSkillState(EStateName.IndividualSkill, this, 1, CurrentPlayer));
        EnterState(EStateName.IndividualSkill);
    }
}

public class IndividualSkillState : ATimerState
{
    private Player player;

    public IndividualSkillState(EStateName stateName, IStateMachine stateMachine, float duration, Player player) : base(stateName, stateMachine, duration)
    {
        nextState = StateName;
        this.player = player;
    }

    public override void OnEnter()
    {
        base.OnEnter();

        Debug.Log($"Entering individual skill for character '{player.CharacterInstance.name}' ({player.Name})");
    }

    public override void OnExit() { }

    public override void OnTimerUpdated(float deltaTime) { }

    public override void OnUpdate(float deltaTime)
    {
        throw new System.NotImplementedException();
    }

    public override void Reset()
    {
        throw new System.NotImplementedException();
    }
}