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

    public State(string name)
    {
        this.name = name;
    }

    public sealed class StateBuilder : BuilderBase<State>
    {

        public override BuilderBase<State> Begin(string name)
        {
            _state = new State(name);
            return this;
        }

        public override State Build()
        {
            return _state;
        }

        public override BuilderBase<State> BuildEnter(Action enter)
        {
            _state.OnEnter = enter;
            return this;
        }

        public override BuilderBase<State> BuildExit(Action exit)
        {
            _state.OnExit = exit;
            return this;
        }

        public override BuilderBase<State> BuildLogic(Action logic)
        {
            _state.OnLogic = logic;
            return this;
        }
    }

}

public abstract class StateBase
{
	public abstract void Enter();
	public abstract void DoLogic();

	public abstract void Exit();
	
}