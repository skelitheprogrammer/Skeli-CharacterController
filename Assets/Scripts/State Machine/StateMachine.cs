using System;
using System.Collections.Generic;

public class StateMachine : State, IStateMachine
{
	private readonly List<State> _states = new List<State>();
	private readonly List<Transition> _transitions = new List<Transition>();

	public State ActiveState { get; private set; }

	public StateMachine(string name) : base(name)
	{
	}

	public class StateMachineBuilder : BuilderBase<StateMachine, StateMachineBuilderFinal>
	{

		public override StateMachineBuilderFinal Begin(string name)
		{
			_state = new StateMachine(name);
			return new StateMachineBuilderFinal();
		}

		public override StateMachineBuilderFinal BuildEnter(Action action)
		{
			_state.OnEnter = action;
			return new StateMachineBuilderFinal();
		}

		public override StateMachineBuilderFinal BuildExit(Action exit)
		{
			_state.OnExit = exit;
			return new StateMachineBuilderFinal();
		}

		public override StateMachineBuilderFinal BuildLogic(Action logic)
		{
			_state.OnLogic = logic;
			return new StateMachineBuilderFinal();
		}
	}

	public sealed class StateMachineBuilderFinal : StateMachine.StateMachineBuilder
	{
		public StateMachine Build() => _state;
	}

	public void AddState(State state)
	{
		_states.Add(state);
	}

	public void AddTransition(Transition transition)
	{
		_transitions.Add(transition);
	}

	public void SetActiveState(State state)
	{
		ActiveState = state;
	}

	public void ChangeState(State state)
	{
		ActiveState?.Exit();
		SetActiveState(state);
		ActiveState?.Enter();
	}

	public void UpdateState()
	{
		ActiveState?.DoLogic();

		foreach (var transition in _transitions)
		{
			if (this == transition.from || ActiveState != transition.from)
			{
				continue;
			}

			if (transition.ShouldTransition())
			{
				ChangeState(transition.to);
			}
		}
	}

	public override void DoLogic()
	{
		if (_states.Count > 0 && ActiveState == null) throw new NullReferenceException($"Set ActiveState of {name}");

		OnLogic?.Invoke();

		foreach (var transition in _transitions)
		{
			if (this != transition.from || ActiveState == transition.from)
			{
				continue;
			}

			if (transition.ShouldTransition())
			{
				ChangeState(transition.to);
			}
		}

		UpdateState();
	}
}
