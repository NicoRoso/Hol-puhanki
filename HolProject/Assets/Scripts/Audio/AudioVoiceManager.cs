using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioVoiceManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        DialogManager.OnVoiceSounded += PlaySound;
    }

    private void OnDisable()
    {
        DialogManager.OnVoiceSounded -= PlaySound;
    }

    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
