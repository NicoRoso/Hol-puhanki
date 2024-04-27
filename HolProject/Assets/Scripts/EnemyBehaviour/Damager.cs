using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damager : MonoBehaviour
{
    [SerializeField] int _damage;
    [SerializeField] bool _destroyObjectOnCollision;
    Collider damageCollider;
    private void Start()
    {
        damageCollider = GetComponent<Collider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //if(other == player)
        //{
            //other.какой-то метод(_damage);
            //damageCollider.enabled = false;
            //уничтожать, если условие
        //}
    }
}
