using System;

public class State : StateBase
{
	protected Action OnEnter;
	protected Action OnLogic;
	protected Action OnExit;

	public readonly string name;

	public override void Enter() => OnEnter?.Invoke();
	public override void DoLogic() => OnLogic?.Invoke();
	public override void Exit() => OnExit?.Invoke();

	protected State(string name) => this.name = name;

	public class StateBuilder
    {
		protected State _state;

		public StateLogic Begin(string name)
        {
			_state = new State(name);
			return new StateLogic(_state);
        }

		public class StateBuild
        {
			protected State _state;

			public StateBuild(State state) => _state = state;

			public State Build() => _state;
		}

		public class StateLogic : StateBuild
        {
            public StateLogic(State state) : base(state) {}

            public StateLogic BuildEnter(Action enter)
			{
				_state.OnEnter = enter;
				return new StateLogic(_state);
			}

			public StateLogic BuildLogic(Action logic)
			{
				_state.OnLogic = logic;
				return new StateLogic(_state);
			}

			public StateLogic BuildExit(Action exit)
			{
				_state.OnExit = exit;
				return new StateLogic(_state);
			}

		}
    }

}