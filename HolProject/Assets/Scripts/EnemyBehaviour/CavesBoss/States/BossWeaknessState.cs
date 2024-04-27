using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWeaknessState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.SwitchBarrierState(false);
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
