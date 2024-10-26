using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DemoManager : MonoBehaviour
{
    private StateMachine m_StateMachine;

    public TMP_Text CurrentStateText;
    public Button InitializeButton;
    public Button NextStateButton;

    private void Start()
    {
        CurrentStateText.text = "";
        InitializeButton.onClick.AddListener(Initialize);
        NextStateButton.onClick.AddListener(NextState);
    }

    private void Update()
    {
        if (m_StateMachine != null)
        {
            m_StateMachine.CurrentState.Update();
        }
    }

    public void Initialize()
    {
        m_StateMachine = new StateMachine();
        m_StateMachine.AddState<State0>(EStates.State0);
        m_StateMachine.AddState<State1>(EStates.State1);
        m_StateMachine.AddState<State2>(EStates.State2);

        m_StateMachine.EnterState(EStates.State0);
        UpdateCurrentStateText();
    }

    public void NextState()
    {
        m_StateMachine.CurrentState.Exit();
        UpdateCurrentStateText();
    }

    public void UpdateCurrentStateText()
    {
        CurrentStateText.text = "State" + m_StateMachine.CurrentStateId;
    }
}

public class StateMachine : AStateMachine
{

}
