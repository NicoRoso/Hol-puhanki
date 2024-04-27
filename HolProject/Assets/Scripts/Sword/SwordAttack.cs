using System;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public static Action OnAttacked;

    private void OnEnable()
    {
        OnAttacked += AttackChecker;
    }

    private void OnDisable()
    {
        OnAttacked -= AttackChecker;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            OnAttacked?.Invoke();
        }
    }

    private void AttackChecker()
    {
        Debug.Log("Attacked function");
    }
}
