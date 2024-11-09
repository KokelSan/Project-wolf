
public class RoundStateMachine : AStateMachine
{
    public override void InitializeMachine()
    {
        SetState(new RoundBeginningState(EStateName.RoundBeginning, this, 1));
        SetState(new SkillsLoopState(EStateName.SkillsLoop, this));
        SetState(new RoundEndingState(EStateName.RoundEnding, this, 1));
    }

    public override void OnStart()
    {
        EnterState(EStateName.RoundBeginning);
    }

    public override void OnUpdate(float deltaTime) { }

    public override void OnExit() { }
}