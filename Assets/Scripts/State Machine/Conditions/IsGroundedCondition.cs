using Zenject;

public class IsGroundedCondition : ConditionBase
{
    [Inject] private CharacterStateData _data;
    public override bool BoolCondition { get; }

    public IsGroundedCondition()
    {
        BoolCondition = _data.isGrounded;
    }

}
