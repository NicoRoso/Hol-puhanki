using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTpFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.StartTpAttack();
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
