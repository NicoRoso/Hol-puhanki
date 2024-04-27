using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSummonState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.SpawnMinions();
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
