using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardAttackState : WizardBaseState
{
    public override void EnterState(WizardStateManager wizard)
    {
        wizard.SetSpeed(0);
        wizard.animator.SetBool("isAttacking",true);
    }
    public override void UpdateState(WizardStateManager wizard)
    {
        wizard.RotateToTarget();
    }
}
