using System;

public class Transition
{
    public readonly State from;
    public readonly State to;

    private readonly Condition _condition;

    public Transition(State from, State to, Func<bool> condition)
    {
        this.from = from;
        this.to = to;

        if (condition == null)
        {
            _condition = null;
            return;
        }

        _condition = new Condition(condition);
    }

    public Transition(State from, State to, Condition condition)
    {
        this.from = from;
        this.to = to;

        if (condition == null)
        {
            _condition = null;
            return;
        }

        _condition = condition;
    }

    public bool ShouldTransition()
    {
        if (_condition == null) return true;

        return _condition.IsMet();
    }
}
