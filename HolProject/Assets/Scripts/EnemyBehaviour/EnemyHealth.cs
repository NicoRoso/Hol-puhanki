using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _iFrameDuration;
    [SerializeField] Slider _hpBar;
    bool damageable = true;
    int health;
    private void Awake()
    {
        health = _maxHealth;
        ShowHP(false);
    }
    public void TakeDamage(int damage)
    {
        if (!damageable) return;
        ShowHP(true);
        health -= damage;
        if (health <= 0)
        {
            ShowHP(false);
            health = 0;
            //смерть
            return;
        }
        //звук
    
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
        _hpBar.value = health/_maxHealth;
    }
    void ShowHP(bool visible)
    {
        _hpBar.gameObject.SetActive(visible);
    }
}
