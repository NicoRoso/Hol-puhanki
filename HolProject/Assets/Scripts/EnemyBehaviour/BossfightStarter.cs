using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossfightStarter : MonoBehaviour
{
    [SerializeField] BossStateManager boss;
    [SerializeField] Gate _gate;
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<PlayerStatSys>(out _))
        {
            boss.bossfightStarted = true;
            _gate.MoveDown();
            Destroy(gameObject);
        }
    }
}
