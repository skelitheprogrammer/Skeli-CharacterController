using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : State, IStateMachine
{
	private readonly List<State> _states = new();
	private readonly List<Transition> _stateTransitions = new();

	public State ActiveState { get; private set; }

	public StateMachine(string name) : base(name) { }

	public class StateMachineBuilder
	{
		public StateMachineLogic Begin(string name) => new(name);
	}

	public class StateMachineBuild
    {
		public readonly StateMachine state;

		public StateMachineBuild(string name) => state = new StateMachine(name);

		public StateMachine Build() => state;
	}

	public class StateMachineLogic : StateMachineBuild
    {
        public StateMachineLogic(string name) : base(name) { }

        public StateMachineLogic BuildEnter(Action enter)
        {
			state.OnEnter = enter;
			return this;
        }

		public StateMachineLogic BuildLogic(Action logic)
		{
			state.OnLogic = logic;
			return this;
		}

		public StateMachineLogic BuildExit(Action exit)
		{
			state.OnExit = exit;
			return this;
		}
	}

    public void AddState(State state) => _states.Add(state);

	public void AddTransition(Transition transition) => _stateTransitions.Add(transition);

    public void UpdateState()
    {
        ActiveState?.DoLogic();
		DoLogic();

        LoopStateMachineTransitions();
        //LoopActiveStateTransitions();
    }

	private void ChangeState(State state)
	{
		ActiveState?.Exit();
		ActiveState = state;

		/*		if (ActiveState == null)
				{
					OnEnter?.Invoke();

					return;
				}*/

		ActiveState.Enter();
	}



	private void LoopStateMachineTransitions()
    {
		foreach (var transition in _stateTransitions)
		{
			if (ActiveState == transition.to) continue;

			TryProceedTransition(transition);
		}
	}

	private void LoopActiveStateTransitions()
    {
		foreach (var transition in _stateTransitions)
		{
            TryProceedTransition(transition);
		}
	}

	private void TryProceedTransition(Transition transition)
    {
		if (transition.ShouldTransition())
		{
			//Debug.LogWarning($"transition: {transition.from.name} {transition.to.name}");
			ChangeState(transition.to);
		}
	}
}
