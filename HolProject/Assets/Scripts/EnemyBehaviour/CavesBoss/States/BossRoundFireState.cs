using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoundFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.StartRoundAttack();
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
