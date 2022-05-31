using System;
using UnityEngine;

namespace Skeli.StateMachine
{
    public class State : StateBase, IEnter, ILogic, IExit
    {
        protected Action OnEnter { get; private set; }
        protected Action OnLogic { get; private set; }
        protected Action OnExit { get; private set; }

        protected State() { }
        protected State(string name) : base(name) { }

        public virtual void Enter() => OnEnter?.Invoke();
        public virtual void DoLogic() => OnLogic?.Invoke();
        public virtual void Exit() => OnExit?.Invoke();
        public class Builder
        {
            private readonly State _state;

            public Builder()
            {
                _state = new State();
            }

            public Builder(string name)
            {
                _state = new State(name);
            }

            public LogicBuilder BuildLogic()
            {
                return new LogicBuilder(_state);
            }
        }

        public class LogicBuilder
        {
            protected readonly State _state;

            public LogicBuilder(State state) => _state = state;

            public LogicBuilder WithEnter(Action enter)
            {
                _state.OnEnter = enter;
                return this;
            }

            public LogicBuilder WithTick(Action logic)
            {
                _state.OnLogic = logic;
                return this;
            }

            public LogicBuilder WithExit(Action exit)
            {
                _state.OnExit = exit;
                return this;
            }

            protected internal State Build() => _state;

            public static implicit operator State(LogicBuilder builder) => builder.Build();
        }
    }
}