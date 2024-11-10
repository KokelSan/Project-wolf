public class GroupSkillsState_SM : AStateMachineState
{
    public GroupSkillsState_SM(EStateName stateName, IStateMachine stateMachine) : base(stateName, stateMachine) { }

    public override void InitializeMachine()
    {
        StartingStateName = EStateName.GroupSkill;
        DefaultNextStateName = EStateName.RoundEnding;

        SetState(new GenericTimerState(StartingStateName, this, 1, EStateName.None));
    }
}