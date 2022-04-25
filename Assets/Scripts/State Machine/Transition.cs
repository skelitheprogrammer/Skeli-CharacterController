using System;
using System.Collections.Generic;

public class Transition
{
    public readonly State from;
    public readonly State to;

    private readonly List<Func<Transition,bool>> _conditions;

    public Transition(State from, State to, params Func<Transition, bool>[] conditions)
    {
        this.from = from;
        this.to = to;

        if (conditions == null)
        {
            _conditions = null;
            return;
        }

        _conditions = new List<Func<Transition, bool>>(conditions);
    }

    public bool ShouldTransition()
    {
        if (_conditions == null) return true;

        if (_conditions.Count == 0) return true;

        if (_conditions.Count == 1) return _conditions[0](this);

        foreach (var condition in _conditions)
        {
            if (!condition(this)) return false;
        }

        return true;
    }
}
