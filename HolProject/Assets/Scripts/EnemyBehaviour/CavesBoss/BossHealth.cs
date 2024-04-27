using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _iFrameDuration;
    [SerializeField] int maxHitsInARow;
    [SerializeField] Slider _hpBar;
    bool damageable = true;
    int health;
    int hitsInARow = 0;
    BossStateManager boss;
    private void Awake()
    {
        health = _maxHealth;
        boss = GetComponent<BossStateManager>();
        ShowHP(false);
    }
    public void TakeDamage(int damage)
    {
        if (!damageable) return;
        health -= damage;
        ShowHP(true);
        if (health <= 0)
        {
            ShowHP(false);
            health = 0;
            boss.SwitchState(boss.bossDeath);
            return;
        }
        boss._animator.SetTrigger("isHit");
        //���� ��� ������
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
    private void Update()
    {
        _hpBar.value = health / _maxHealth;
    }
    void ShowHP(bool visible)
    {
        _hpBar.gameObject.SetActive(visible);
    }
}