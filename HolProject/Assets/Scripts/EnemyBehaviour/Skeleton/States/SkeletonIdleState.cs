using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonBaseState
{
    public override void EnterState(SkeletonStateManager skelet)
    {
        skelet.SetSpeed(0);
    }
    public override void UpdateState(SkeletonStateManager skelet)
    {

    }
}
