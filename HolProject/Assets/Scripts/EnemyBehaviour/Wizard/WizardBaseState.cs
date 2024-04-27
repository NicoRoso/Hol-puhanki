using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class WizardBaseState
{
    public abstract void EnterState(WizardStateManager wizard);
    public abstract void UpdateState(WizardStateManager wizard);
}
