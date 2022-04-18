using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class State
{
	public Action<State> OnStateEnter;
	public Action<State> OnStateExit;
	
	public Action<State> OnStateLogic;
	
}

public class StateCondition
{
	
}

public class StateTransition
{
	
	public void AddCondition()
	
	{
		
	}
	
}


public interface IStateMachine
{
	
	void AddTransition(State from, State to,StateTransition transition);
	void AddState(State state);
	
	void ChangeState(State state);
	void UpdateState();
}

public class StateMachine : State, IStateMachine
{
	private List<State> _states = new List<State>();
	private State _currentState;
	
	public void AddState(State state)
	{
		_states.Add(state);
	}

	public void AddTransition(State from, State to, StateTransition transition)
	{

	}

	public void ChangeState(State state)
	{
		
	}

	public void UpdateState()
	{
		_currentState.OnStateLogic?.Invoke(_currentState);
		
		
			
	}
}
