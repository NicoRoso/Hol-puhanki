using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _iFrameDuration;
    [SerializeField] Slider _hpBar;
    Animator animator;
    bool damageable = true;
    int health;
    private void Awake()
    {
        health = _maxHealth;
        ShowHP(false);
        animator = GetComponent<Animator>();
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
            animator.SetTrigger("isDead");
            return;
        }
        //����
    
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
    public int GetHealth()
    {
        return health;
    }

}
