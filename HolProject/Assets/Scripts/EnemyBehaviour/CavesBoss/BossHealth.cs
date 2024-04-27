using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _iFrameDuration;
    [SerializeField] int maxHitsInARow;
    bool damageable = true;
    int health;
    int hitsInARow = 0;
    BossStateManager boss;
    private void Awake()
    {
        health = _maxHealth;
        boss = GetComponent<BossStateManager>();
    }
    public void TakeDamage(int damage)
    {
        if (!damageable) return;
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            boss.SwitchState(boss.bossDeath);
            return;
        }
        boss._animator.SetTrigger("isHit");
        //звук ещё разный
        hitsInARow++;
        if (hitsInARow >= maxHitsInARow)
        {
            hitsInARow = 0;
            boss.SwitchBarrierState(true);
            StartCoroutine(boss.WaitforSecondsBeforeNexstState(1));
        }
        StartCoroutine(iFrameTimer());
    }
    IEnumerator iFrameTimer()
    {
        damageable = false;
        yield return new WaitForSeconds(_iFrameDuration);
        damageable = true;
    }
}
