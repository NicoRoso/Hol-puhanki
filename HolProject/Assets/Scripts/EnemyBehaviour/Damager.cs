using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] bool _destroyObjectOnCollision;
    Collider damageCollider;
    public Action playerConnect;
    private void Start()
    {
        damageCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerStatSys>(out PlayerStatSys player))
        {
            //player - health
            if(_destroyObjectOnCollision)
            {
                playerConnect?.Invoke();
                Destroy(gameObject);
            }
            else
            {
                GetComponent<AudioManager>().Play("hit");
                damageCollider.enabled = false;
            }
        }
    }
}
