using System;

public class State : StateBase
{
	public Action OnEnter;
	public Action OnLogic;
	public Action OnExit;

	public override void Enter() => OnEnter?.Invoke();
	public override void DoLogic() => OnLogic?.Invoke();
	public override void Exit() => OnExit?.Invoke();
	
	
}

public abstract class StateBase
{
	public abstract void Enter();
	public abstract void DoLogic();

	public abstract void Exit();
	
	protected abstract class Builder<T> 
	{
		protected StateBase _State;
		
		public abstract Builder<T> Enter(T item);
		public abstract StateBase Build();
		
	}
}