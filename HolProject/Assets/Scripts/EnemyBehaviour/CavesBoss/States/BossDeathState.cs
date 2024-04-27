using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    public override void EnterState(BossStateManager boss)
    {
        boss._animator.SetTrigger("isDead");
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
