using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCurveFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.StartCurveAttack();
    }
    public override void UpdateState(BossStateManager boss)
    {
        boss.IncreaseRotateSpeed();
    }
}
