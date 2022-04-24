using System;

public class State
{
    public Action OnEnter;
    public Action OnLogic;
    public Action OnExit;

    public virtual void Enter() => OnEnter?.Invoke();
    public virtual void DoLogic() => OnLogic?.Invoke();

    public virtual void Exit() => OnExit?.Invoke();
}
