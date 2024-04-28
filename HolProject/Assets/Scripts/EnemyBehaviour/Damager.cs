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
            Debug.Log("прилетела плюха размером в " + _damage);
            if(_destroyObjectOnCollision)
            {
                playerConnect?.Invoke();
                //player.GetComponent<AudioManager>().Play("fireballConnect");
                Destroy(gameObject);
            }
            else
            {
                damageCollider.enabled = false;
            }
        }
    }
}
