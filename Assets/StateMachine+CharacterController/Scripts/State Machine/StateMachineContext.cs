using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachineContext : IStateMachine
{
	private readonly List<StateMachine> _stateMachines = new();
    private readonly List<Transition> _transitions = new();
	public StateMachine ActiveStateMachine { get; private set; }

	private bool _initialized = false;

    public void AddState(State state)
    {
		_stateMachines.Add((StateMachine)state);
    }

	public void AddTransition(Transition transition)
	{
		_transitions.Add(transition);
	}

    public void UpdateState()
    {
		if (ActiveStateMachine == null) throw new NullReferenceException($"Initialize State Machine Context");
		
		//Debug.Log($"{ActiveStateMachine.name} {ActiveStateMachine.ActiveState?.name}");
		
		ActiveStateMachine.DoLogic();

		foreach (var transition in _transitions)
		{
			if (ActiveStateMachine != transition.from) continue;

			TryProceedTransition(transition);
		}
	}

	public void Init(StateMachine stateMachine)
    {
		if (!_stateMachines.Contains(stateMachine)) throw new NullReferenceException($"Theres no {stateMachine.name} in State Machine Context");

		if (_initialized) throw new InvalidOperationException("State Machine Context already initialized");

		_initialized = true;
		ActiveStateMachine = stateMachine;
	}

	private void ChangeState(State state)
	{
		ActiveStateMachine?.Exit();
		ActiveStateMachine = (StateMachine)state;
		ActiveStateMachine?.Enter();
	}

	private void TryProceedTransition(Transition transition)
	{
		if (transition.ShouldTransition())
		{
			Debug.Log($"{transition.from.name} {transition.to.name}");
			ChangeState(transition.to);
		}
	}
}