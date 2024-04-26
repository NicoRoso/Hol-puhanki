using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCurveFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss.StartCurveAttack(boss._curveAttackWavesAmount, boss._curveAttackWavesSpawnDelay, boss._curveFireballRotationSpeed,boss._curveFireballFlyAwaySpeed);
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
