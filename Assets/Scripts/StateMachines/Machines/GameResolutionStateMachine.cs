using System;

public class GameResolutionStateMachine : AStateMachine
{
    private GameManager gameManager;

    public GameResolutionStateMachine(GameManager gameManager) : base() 
    { 
        this.gameManager = gameManager;
    }

    public override void InitializeMachine()
    {
        AddState(new GameRevealTimerState(EStateName.GameReveal, this, 2));
        AddState(new IndividualSkillsTimerState(EStateName.IndividualSkills, this, 2));
        AddState(new GroupSkillsTimerState(EStateName.GroupSkills, this, 2));
    }

    public override void StartMachine(Action onMachineCompleted)
    {
        base.StartMachine(onMachineCompleted);

        EnterState(EStateName.GameReveal);
    }
}