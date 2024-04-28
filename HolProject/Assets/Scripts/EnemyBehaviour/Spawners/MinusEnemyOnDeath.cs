using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinusEnemyOnDeath : MonoBehaviour
{
    EnemyHealth health;
    bool flag = false;
    private void Start()
    {
        health = GetComponent<EnemyHealth>();
    }
    private void Update()
    {
        if((health.GetHealth() <= 0) && !flag)
        {
            flag = true;
            StaticWavesValue.MinusEnemy();
        }
    }
}
