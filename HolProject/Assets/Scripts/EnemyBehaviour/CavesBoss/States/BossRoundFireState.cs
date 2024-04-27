using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoundFireState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss._animator.SetTrigger("centerAttack");
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
