using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonWalkState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager skelet)
    {
        skelet.animator.SetTrigger("activateBrain");
        skelet.SetSpeed(skelet._walkSpeed);
    }
    public override void UpdateState(SkeletonStateManager skelet)
    {
        if((skelet._attackDistance >= skelet.GetRemainingDistance()) && (skelet.GetRemainingDistance() != 0))
        {
            skelet.SwitchState(skelet.skeletonAttack);
        }
    }
}