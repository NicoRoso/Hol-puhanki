using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballCurveAttackState : FireballBaseState
{
    public override void EnterState(FireballStateManager fireball)
    {
        fireball.IncreaseSizeToNormal(0.4f);
        fireball.StartCurveAttackControl();
    }
    public override void UpdateState(FireballStateManager fireball)
    {
        Vector3 newCenterRot = fireball._rotateCenter.position;
        newCenterRot.y = 1;
        fireball.transform.RotateAround(newCenterRot, new Vector3(0, 1, 0), fireball._rotateSpeed * Time.deltaTime);
        fireball.GoFurtherFromCenter();
    }
}
