using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCurveFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss._animator.SetTrigger("centerAttack");
    }
    public override void UpdateState(BossStateManager boss)
    {
        boss.IncreaseRotateSpeed();
    }
}
