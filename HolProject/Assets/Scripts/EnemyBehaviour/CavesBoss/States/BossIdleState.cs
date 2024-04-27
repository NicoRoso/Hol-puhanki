using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdleState : BossBaseState
{
    enum nextAttackType { fireRound, fireCurve, fireTp, Spawn }
    nextAttackType nextAttack;
    BossBaseState nextsSate;
    BossStateManager stateManager;
    public override void EnterState(BossStateManager boss)
    {
        stateManager = boss;
        boss.changeAttack = null;
        boss.changeAttack += StartNextAttack;
    }
    public override void UpdateState(BossStateManager boss)
    {
        if(boss.bossfightStarted)
        {
            boss.bossfightStarted = false;
            boss.SwitchBarrierState(true);
            boss.TryMakeNextAttack();
            return;
        }

    }
    void ChooseNextAttack()
    {
        int randomNumber = Random.Range(0,4);
        switch (randomNumber)
        {
            case 0:
                {
                    nextsSate = stateManager.bossRoundFire;
                    return;
                }
            case 1:
                {
                    nextsSate = stateManager.bossCurveFire;
                    return;
                }
            case 2:
                {
                    nextsSate = stateManager.bossTpFire;
                    return;
                }
            case 3:
                {
                    nextsSate = stateManager.bossSummon;
                    return;
                }
            default:
                {
                    nextsSate = stateManager.bossTpFire;
                    return;
                }
        }
    }
    void StartNextAttack()
    {
        ChooseNextAttack();
        while (stateManager.currentState == nextsSate)
        {
            ChooseNextAttack();
        }
        stateManager.SwitchState(nextsSate);
    }
}
