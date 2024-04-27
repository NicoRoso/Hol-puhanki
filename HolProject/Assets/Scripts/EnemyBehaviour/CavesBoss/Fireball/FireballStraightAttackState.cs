using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballStraightAttackState : FireballBaseState
{
    public override void EnterState(FireballStateManager fireball)
    {
        fireball.IncreaseSizeToNormal(0.1f);
        fireball.StartStraightAttack();
    }
    public override void UpdateState(FireballStateManager fireball)
    {

    }
}
