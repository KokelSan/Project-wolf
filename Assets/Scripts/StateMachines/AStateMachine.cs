using System.Collections.Generic;
using UnityEngine;

public abstract class AStateMachine
{
    public Dictionary<EStates, IState> States;
    public EStates CurrentStateId;
    public IState CurrentState;

    public AStateMachine()
    {
        States = new Dictionary<EStates, IState>();
        CurrentStateId = EStates.None;
        CurrentState = null;
    }

    public AStateMachine(EStates stateKey, IState state)
    {
        States = new Dictionary<EStates, IState>();
        States.Add(stateKey, state);
        CurrentStateId = stateKey;
        CurrentState = state;
    }

    public void AddState<T>(EStates stateKey) where T : IState, new()
    {
        if (!States.ContainsKey(stateKey))
        {
            States.Add(stateKey, new T());
        }
        else
        {
            Debug.Log(stateKey.ToString() + " already present in state machine");
        }        
    }

    public void EnterState(EStates stateKey)
    {
        if (States.ContainsKey(stateKey))
        {
            CurrentStateId = stateKey;
            CurrentState = States[CurrentStateId];
            States[stateKey].Enter(this);
        }
        else
        {
            Debug.Log("Error switching to state " + stateKey.ToString());
        }               
    }
}
