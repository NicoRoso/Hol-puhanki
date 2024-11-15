using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int _maxHealth;
    [SerializeField] float _iFrameDuration;
    [SerializeField] Slider _hpBar;
    [SerializeField] int _timeBeforeGoDown;
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
            if(!isDead) 
            {
                animator.SetTrigger("isDead");
                StartCoroutine(StartFadeCountdown());
            }
            isDead = true;
            foreach(Collider collider in GetComponents<Collider>())
            {
                collider.enabled = false;
            }
            return;
        }
        //����
    
        StartCoroutine(iFrameTimer());
    }
    IEnumerator StartFadeCountdown()
    {
        yield return new WaitForSeconds(_timeBeforeGoDown);
        transform.DOMoveY(transform.position.y - 2,5);
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
        yield break;
    }
    IEnumerator iFrameTimer()
    {
        damageable = false;
        yield return new WaitForSeconds(_iFrameDuration);
        damageable = true;
    }
    private void Update()
    {
        _hpBar.value = (float)health/_maxHealth;
    }
    void ShowHP(bool visible)
    {
        _hpBar.gameObject.SetActive(visible);
    }
    public int GetHealth()
    {
        return health;
    }
    bool isDead = false;

}
