using System;

public class StateBuilder
{
    private State _state;

    public State Build()
    {
        State tempState = _state;
        _state = new State();

        return tempState;
    }

    public StateBuilder Enter(Action enter) 
    {
        _state.OnEnter = enter;
        return this;
    }

    public StateBuilder Logic(Action logic)
    {
        _state.OnLogic = logic;
        return this;
    }

    public StateBuilder Exit(Action exit)
    {
        _state.OnExit = exit;
        return this;
    }
}
