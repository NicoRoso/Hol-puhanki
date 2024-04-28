using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathState : BossBaseState
{
    bool isDead = false;
    public override void EnterState(BossStateManager boss)
    {
        if(!isDead) boss._animator.SetTrigger("isDead");
        isDead = true;
        // death effects?
    }
    public override void UpdateState(BossStateManager boss)
    {

    }
}
