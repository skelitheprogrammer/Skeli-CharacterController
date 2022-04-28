using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : State, IStateMachine
{
	private readonly List<State> _states = new List<State>();
	private readonly List<Transition> _stateTransitions = new List<Transition>();

	public State ActiveState { get; private set; }

	public StateMachine(string name) : base(name)
	{
	}

	public class StateMachineBuilder : BuilderBase<StateMachine, StateMachineBuilderFinal>
	{
		public override BuilderBase<StateMachine, StateMachineBuilderFinal> Begin(string name)
		{
			_state = new StateMachine(name);
			return this;
		}

		public override StateMachineBuilderFinal BuildEnter(Action action)
		{
			_state.OnEnter = action;
			return new StateMachineBuilderFinal(_state);
		}

		public override StateMachineBuilderFinal BuildExit(Action exit)
		{
			_state.OnExit = exit;
			return new StateMachineBuilderFinal(_state);
		}

		public override StateMachineBuilderFinal BuildLogic(Action logic)
		{
			_state.OnLogic = logic;
			return new StateMachineBuilderFinal(_state);
		}
	}

	public sealed class StateMachineBuilderFinal : StateMachineBuilder
	{
		public StateMachineBuilderFinal(StateMachine state)
        {
			_state = state;
        }

		public StateMachine Build() => _state;
	}

	public void AddState(State state) => _states.Add(state);

	public void AddTransition(Transition transition) => _stateTransitions.Add(transition);

	public void SetActiveState(State state) => ActiveState = state;

	public void ChangeState(State state)
	{
		ActiveState?.Exit();
		SetActiveState(state);
		ActiveState?.Enter();
	}

	public void UpdateState()
	{
		ActiveState?.DoLogic();

		foreach (var transition in _stateTransitions)
		{
            if (this == transition.from || ActiveState != transition.from)
            {
                continue;
            }

            /*            if (transition.from == ActiveState && transition.to == this)
						{
							Debug.Log("1");
							ChangeState(null);
							return;
						}*/

            TryProceedTransition(transition);
		}
	}

	public override void DoLogic()
	{
		OnLogic?.Invoke();

		foreach (var transition in _stateTransitions)
		{
            if (this != transition.from || ActiveState == transition.from)
            {
                continue;
            }

            TryProceedTransition(transition);
		}
		
		UpdateState();
	}

	private void TryProceedTransition(Transition transition)
    {
		if (transition.ShouldTransition())
		{
			Debug.Log($"{transition.from.name} : {transition.to.name}");
			ChangeState(transition.to);
		}
	}
}
