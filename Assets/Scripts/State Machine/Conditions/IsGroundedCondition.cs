using System;
using Zenject;

public class IsGroundedCondition : Condition
{
    [Inject] private readonly CharacterStateData _data;

    public IsGroundedCondition(Func<bool> condition) : base(condition)
    {
        condition = () => _data.isGrounded;
    }
}
