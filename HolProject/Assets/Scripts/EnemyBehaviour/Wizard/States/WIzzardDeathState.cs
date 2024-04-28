using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WIzzardDeathState : WizardBaseState
{
    public override void EnterState(WizardStateManager wizard)
    {
        //wizard.animator.SetTrigger("isDead");
        wizard.audioManager.Play("death");
    }
    public override void UpdateState(WizardStateManager wizard)
    {

    }
}
