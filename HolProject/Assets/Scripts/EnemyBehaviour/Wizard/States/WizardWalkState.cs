using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardWalkState : WizardBaseState
{
    public override void EnterState(WizardStateManager wizard)
    {
        wizard.SetSpeed(wizard._moveSpeed);
    }
    public override void UpdateState(WizardStateManager wizard)
    {
        if(wizard.GetRemainingDistance() <= wizard._attackDistance)
        {
            wizard.SwitchState(wizard.wizardAttack);
        }
    }
}
