using System;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public static Action OnAttacked;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Respawn")
        {
            OnAttacked?.Invoke();
        }
    }
}
