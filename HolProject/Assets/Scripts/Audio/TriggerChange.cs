using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerChange : MonoBehaviour
{
    [SerializeField] private AudioClip _music;

    public static Action<AudioClip, float, float> OnMusicSounded;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            OnMusicSounded?.Invoke(_music, 1f, 1f);
        }
    }
}
