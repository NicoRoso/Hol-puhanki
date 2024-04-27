using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager skelet)
    {
        skelet.SetSpeed(0);
        skelet.animator.SetBool("isAttacking",true);
    }
    public override void UpdateState(SkeletonStateManager skelet)
    {

    }
}
