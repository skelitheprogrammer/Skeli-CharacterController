using System;

public class Condition
{
    private readonly Func<bool> _condition;

    public Condition(Func<bool> condition)
    {
        _condition = condition;
    }

    public bool IsMet() 
    {
        return _condition.Invoke();
    }
}
