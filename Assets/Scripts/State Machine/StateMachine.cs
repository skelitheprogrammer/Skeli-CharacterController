using System;
using System.Collections.Generic;

public  class StateMachine : State
{
	private readonly List<State> _states = new List<State>();
	private readonly List<Transition> _transitions = new List<Transition>();
	public State ActiveState { get; private set; }

    public class StateMachineBuilder : BuilderBase<StateMachine>
    {
        public override BuilderBase<StateMachine> Begin()
        {
			_state = new StateMachine();
			return this;
        }

        public override StateMachine Build()
        {
			return _state;
        }

        public override BuilderBase<StateMachine> BuildEnter(Action action)
        {
			_state.OnEnter = action;
			return this;
        }

		public override BuilderBase<StateMachine> BuildExit(Action exit)
		{
			_state.OnExit = exit;
			return this;
		}

		public override BuilderBase<StateMachine> BuildLogic(Action logic)
		{
			_state.OnLogic = logic;
			return this;
		}
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

	private void ChangeState(State state)
	{
		ActiveState?.Exit();
		SetActiveState(state);
		ActiveState?.Enter();
	}

	public override void DoLogic()
	{
		OnLogic?.Invoke();

		if (ActiveState == null) throw new NullReferenceException($"Set starter state in {GetType()}");
		ActiveState?.DoLogic();

		foreach(var transition in _transitions)
		{
			if (ActiveState == transition.to) return;

			if (transition.ShouldTransition()) 
			{
				ChangeState(transition.to);
			}
		}
	}
}
