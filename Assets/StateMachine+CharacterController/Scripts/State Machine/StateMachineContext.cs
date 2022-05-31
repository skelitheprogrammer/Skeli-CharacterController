using System;
using System.Collections.Generic;
using UnityEngine;

namespace Skeli.StateMachine
{
    public sealed class StateMachineContext : IStateMachine
    {
        private readonly List<StateMachine> _stateMachines = new();
        private readonly List<Transition> _transitions = new();
        
        private StateMachine _activeStateMachine;

        private bool _initialized = false;

        public string ActiveStateMachineName => _activeStateMachine.Name;

        public void AddStateMachine(StateMachine state)
        {
            _stateMachines.Add(state);
        }

        public void AddTransition(Transition transition)
        {
            _transitions.Add(transition);
        }

        public void UpdateState()
        {
            if (_activeStateMachine == null) throw new NullReferenceException($"Initialize State Machine Context");

            _activeStateMachine.DoLogic();

            foreach (var transition in _transitions)
            {
                if (_activeStateMachine != transition.from) continue;

                TryProceedTransition(transition);
            }
        }

        public void Init(StateMachine stateMachine)
        {
            if (!_stateMachines.Contains(stateMachine)) throw new NullReferenceException($"Theres no {stateMachine.Name} in State Machine Context");

            if (_initialized) throw new InvalidOperationException("State Machine Context already initialized");

            _initialized = true;
            SetState(stateMachine);
        }

        private void SetState(StateMachine stateMachine)
        {
            _activeStateMachine = stateMachine;
            _activeStateMachine?.Enter();
        }

        private void ChangeState(StateMachine stateMachine)
        {
            _activeStateMachine?.Exit();
            SetState(stateMachine);
        }

        private void TryProceedTransition(Transition transition)
        {
            if (transition.ShouldTransition())
            {
                ChangeState((StateMachine)transition.to);
            }
        }
    }
}