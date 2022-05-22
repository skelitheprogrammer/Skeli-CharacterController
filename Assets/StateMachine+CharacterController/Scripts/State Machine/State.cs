using System;

public class State
{
	public readonly string Name;

	protected internal Action OnEnter;
	protected internal Action OnLogic;
	protected internal Action OnExit;

	public virtual void Enter() => OnEnter?.Invoke();
	public virtual void DoLogic() => OnLogic?.Invoke();
	public virtual void Exit() => OnExit?.Invoke();

	public State(string name) => Name = name;

}

#region Builder
public class StateBuilder
{
    private State _state;

    public StateLogicBuild Begin(string name)
    {
        _state = new State(name);
        return new StateLogicBuild(_state);
    }

    public class StateBuild : StateLogic
    {
        public StateBuild(State state) : base(state)
        {
        }

        public State Build() => _state;
    }
    public class StateLogicBuild
    {
        private readonly State _state;

        public StateLogicBuild(State state)
        {
            _state = state;
        }

        public StateLogic BuildLogic()
        {
            return new StateLogic(_state);
        }
    }

    public class StateLogic
    {
        protected readonly State _state;

        public StateLogic(State state)
        {
            _state = state;
        }

        public StateBuild WithEnter(Action enter)
        {
            _state.OnEnter = enter;
            return new StateBuild(_state);
        }

        public StateBuild WithLogic(Action logic)
        {
            _state.OnLogic = logic;
            return new StateBuild(_state);
        }

        public StateBuild WithExit(Action exit)
        {
            _state.OnExit = exit;
            return new StateBuild(_state);
        }

    }
}
#endregion


