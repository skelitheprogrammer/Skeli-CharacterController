using System;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : State, IStateMachine
{
    private readonly List<State> _states = new();
    private readonly List<Transition> _stateTransitions = new();

    public State ActiveState { get; private set; }

    public StateMachine(string name) : base(name) { }

    public void AddState(State state) => _states.Add(state);

    public void AddTransition(Transition transition) => _stateTransitions.Add(transition);

    public void SetStartState(State state)
    {
        ActiveState = state;
    }

    public override void DoLogic()
    {
        base.DoLogic();
        UpdateState();
    }

    public void UpdateState()
    {
        //Debug.Log($"{name} {ActiveState?.name}");
        ActiveState?.DoLogic();

        LoopStateMachineTransitions();
    }

    private void ChangeState(State state)
    {
        ActiveState?.Exit();

        if (state == this || state == null)
        {
            ActiveState = null;
            return;
        }

        ActiveState = state;
        ActiveState?.Enter();
    }

    private void LoopStateMachineTransitions()
    {
        foreach (var transition in _stateTransitions)
        {
            if (ActiveState == transition.to) continue;

            TryProceedTransition(transition);
            return;
        }
    }

    private void TryProceedTransition(Transition transition)
    {
        if (transition.ShouldTransition())
        {
            //Debug.LogWarning($"transition: {transition.from?.name} {transition.to.name}");
            ChangeState(transition.to);
        }
    }
}

#region Builder
public class StateMachineBuilder
{
    private StateMachine _stateMachine;

    public StateMachineLogicBuild Begin(string name)
    {
        _stateMachine = new StateMachine(name);
        return new StateMachineLogicBuild(_stateMachine);
    }

    public class StateMachineLogicBuild
    {
        private readonly StateMachine _stateMachine;

        public StateMachineLogicBuild(StateMachine state)
        {
            _stateMachine = state;
        }

        public StateMachineLogic BuildLogic()
        {
            return new StateMachineLogic(_stateMachine);
        }

        public StateMachine Build() => _stateMachine;
    }

    public class StateMachineLogic
    {
        protected readonly StateMachine _stateMachine;

        public StateMachineLogic(StateMachine state)
        {
            _stateMachine = state;
        }

        public StateMachineLogicBuilder WithEnter(Action enter)
        {
            _stateMachine.OnEnter = enter;
            return new StateMachineLogicBuilder(_stateMachine);
        }

        public StateMachineLogicBuilder WithTick(Action logic)
        {
            _stateMachine.OnLogic = logic;
            return new StateMachineLogicBuilder(_stateMachine);
        }

        public StateMachineLogicBuilder WithExit(Action exit)
        {
            _stateMachine.OnExit = exit;
            return new StateMachineLogicBuilder(_stateMachine);
        }

    }

    public class StateMachineLogicBuilder : StateMachineLogic
    {
        public StateMachineLogicBuilder(StateMachine state) : base(state) { }

        public StateMachine Build() => _stateMachine;
    }
}
#endregion