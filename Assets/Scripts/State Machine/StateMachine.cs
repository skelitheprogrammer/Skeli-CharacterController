using System;
using System.Collections.Generic;

public class StateMachine : State
{
    private List<State> _states = new List<State>();
    private List<Transition> _transitions = new List<Transition>();

    public State ActiveState { get; private set; }

    public void AddState(State state)
    {
        _states.Add(state);
    }

    public void AddTransition(Transition transition)
    {
        _transitions.Add(transition);
    }

    public void SetActiveState(State state)
    {
        if (_states.Contains(state))
        {
            ChangeState(state);
        }
    }

    private void ChangeState(State state)
    {
        ActiveState?.Exit();
        ActiveState = state;
        ActiveState?.Enter();
    }

    public override void DoLogic()
    {
        OnLogic?.Invoke();

        if (ActiveState == null) throw new NullReferenceException($"Set starter state in {GetType().Name}");
        ActiveState?.DoLogic();

        foreach(var transition in _transitions)
        {
            if (transition.ShouldTransition()) 
            {
                ChangeState(transition.to);
            }
        }
    }
}
