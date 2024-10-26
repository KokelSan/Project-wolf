using UnityEngine;

public class State0 : IState
{
    private AStateMachine m_StateMachine;
    private float m_EntryTime;

    public float Duration = 3f;
    private float m_TimeCount = 0;

    public void Enter(AStateMachine stateMachine)
    {
        Debug.Log("Entering State0");

        m_StateMachine = stateMachine;
        m_EntryTime = Time.time;
        m_TimeCount = 0;
        Debug.Log(Duration);
    }

    public void Update()
    {
        if (Time.time > m_EntryTime + m_TimeCount + 1)
        {
            m_TimeCount++;
            Debug.Log(Duration - m_TimeCount);
        }

        if (Time.time > m_EntryTime + Duration)
        {
            Exit();
        }        
    }

    public void Exit()
    {
        Debug.Log("Exiting State0...");        
        m_StateMachine.EnterState(EStates.State1);
    }
}