using UnityEngine;

public class State2 : IState
{
    private AStateMachine m_StateMachine;

    public void Enter(AStateMachine stateMachine)
    {
        Debug.Log("Entering State2");

        m_StateMachine = stateMachine;
    }

    public void Update()
    {

    }

    public void Exit()
    {
        Debug.Log("Exiting State2...");
        m_StateMachine.EnterState(EStates.State0);
    }
}