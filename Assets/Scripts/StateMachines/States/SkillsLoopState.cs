using System.Linq;

public class SkillsLoopState : AState
{
    private IndividualSkillsStateMachine individualSkillsStateMachine;
    private GroupSkillsStateMachine groupSkillsStateMachine;

    public SkillsLoopState(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void OnEnter()
    {
        if (GameManager.Instance.PlayersWithIndividualSkills.Any())
        {
            StartIndividualSkillsMachine();
        }
        else
        {
            StartGroupSkillsMachine();
        }        
    }

    public override void Update(float deltaTime)
    {
        individualSkillsStateMachine?.UpdateMachine(deltaTime);
        groupSkillsStateMachine?.UpdateMachine(deltaTime);
    }

    public override void OnExit() { }

    private void StartIndividualSkillsMachine()
    {
        individualSkillsStateMachine = new IndividualSkillsStateMachine();
        individualSkillsStateMachine.StartMachine(OnIndividualSkillsCompleted);
    }    

    private void OnIndividualSkillsCompleted()
    {
        StartGroupSkillsMachine();
    }

    private void StartGroupSkillsMachine()
    {
        groupSkillsStateMachine = new GroupSkillsStateMachine();
        groupSkillsStateMachine.StartMachine(OnGroupSkillsCompleted);
    }

    private void OnGroupSkillsCompleted()
    {
        Exit(EStateName.RoundEnding);
    }

    public override void OnUpdate(float deltaTime) { }
    public override void Reset() { }
}
