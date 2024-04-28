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

    [SerializeField] private GameObject _holyBook;

    public int GetHealth()
    {
        return health;
    }
    private void Awake()
    {
        _holyBook.SetActive(false);
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
            _holyBook.SetActive(true);
            ShowHP(false);
            health = 0;
            boss.SwitchState(boss.bossDeath);
            foreach (Collider collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
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
    private void Update()
    {
        //Debug.Log(health + " " + (float)((float)health / (float)_maxHealth) + " " + _maxHealth);
        _hpBar.value = (float)((float)health / (float)_maxHealth);
    }
    void ShowHP(bool visible)
    {
        _hpBar.gameObject.SetActive(visible);
    }
}
