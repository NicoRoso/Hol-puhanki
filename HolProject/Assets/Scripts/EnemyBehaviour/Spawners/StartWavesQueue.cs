using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartWavesQueue : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<PlayerStatSys>(out _))
        {
            playerCollided?.Invoke();
            playerCollided = null;
            Destroy(gameObject);
        }
    }
    public Action playerCollided;
}
