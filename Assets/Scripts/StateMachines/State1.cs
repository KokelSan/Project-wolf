using UnityEngine;

public class State1 : IState
{
    private AStateMachine m_StateMachine;

    public void Enter(AStateMachine stateMachine)
    {
        Debug.Log("Entering State1");

        m_StateMachine = stateMachine;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting State1...");
        m_StateMachine.EnterState(EStates.State2);
    }
}