using UnityEngine;

public abstract class ATimerState : AState
{
    protected float duration = 3f;
    protected float timer = 0f;
    protected EStateName nextState = EStateName.None;

    public ATimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine)
    {
        this.duration = duration;
    }

    public override void OnEnter()
    {
        timer = 0f;
    }

    public override void Update(float deltaTime) 
    {
        if (!IsCurrentState) return;

        timer += deltaTime;

        //Debug.Log($"{StateName.ToString()} : {(int)timer}");

        if (timer >= duration)
        {
            Exit(nextState);
        }        
    }
}

public class GameRevealTimerState : ATimerState
{
    public GameRevealTimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine, duration)
    {
        nextState = EStateName.IndividualSkills;
    }
}

public class IndividualSkillsTimerState : ATimerState
{
    public IndividualSkillsTimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine, duration)
    {
        nextState = EStateName.GroupSkills;
    }
}

public class GroupSkillsTimerState : ATimerState
{
    public GroupSkillsTimerState(EStateName stateName, IStateMachine stateMachine, float duration) : base(stateName, stateMachine, duration)
    {
    }
}