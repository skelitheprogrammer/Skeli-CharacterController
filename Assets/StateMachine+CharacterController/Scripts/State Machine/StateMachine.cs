using System.Collections.Generic;

namespace Skeli.StateMachine
{
    public sealed class StateMachine : State, IStateMachine
    {
        private readonly List<State> _states = new();
        private readonly List<Transition> _stateTransitions = new();

        private State _entryState;
        private State _activeState;

        public string ActiveStateName => _activeState.Name;

        private StateMachine() : base() { }
        private StateMachine(string name) : base(name) { }

        public void AddState(State state) => _states.Add(state);

        public void AddTransition(Transition transition) => _stateTransitions.Add(transition);
        public void SetEntryState(State state)
        {
            _entryState = state;
        }

        public override void Enter()
        {
            ChangeState(_entryState);
            base.Enter();
        }

        public override void DoLogic()
        {
            base.DoLogic();
            UpdateState();
        }

        public override void Exit()
        {
/*            if (ActiveState != null && ActiveState is StateMachine)
            {
                (ActiveState as StateMachine).ChangeState(null);
            }*/

            ChangeState(null);
            base.Exit();

        }

        public void UpdateState()
        {
            _activeState?.DoLogic();

            LoopStateMachineTransitions();
        }

        public void ResetState()
        {
            _activeState?.Exit();
            _activeState = null;
        }

        private void ChangeState(State state)
        {
            if (state == null && _activeState == null)
            {
                return;
            }

            _activeState?.Exit();

            if (state == null)
            {
                _activeState = null;
                return;
            }

            _activeState = state;
            _activeState?.Enter();
        }

        private void LoopStateMachineTransitions()
        {
            foreach (var transition in _stateTransitions)
            {
                if (_activeState == transition.to) continue;

                TryProceedTransition(transition);

                return;
            }
        }

        private void TryProceedTransition(Transition transition)
        {
            if (transition.ShouldTransition()) ChangeState(transition.to);
        }
        public class StateMachineBuilder
        {
            private readonly StateMachine _sm;

            public StateMachineBuilder()
            {
                _sm = new StateMachine();
            }

            public StateMachineBuilder(string name)
            {
                _sm = new StateMachine(name);
            }

            public LogicBuilder BuildLogic()
            {
                return new LogicBuilder(_sm);
            }
        }

        public static implicit operator StateMachine(LogicBuilder builder) => (StateMachine)builder.Build();
    }
}