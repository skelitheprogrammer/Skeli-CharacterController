using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class IsGroundedCondition : ConditionBase
{
    [Inject] private CharacterStateData _data;

    public override bool IsMet()
    {
        return _data.isGrounded;
    }
}
