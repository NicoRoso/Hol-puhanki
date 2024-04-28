using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioMusicManager : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        TriggerChange.OnMusicSounded += PlaySound;
    }

    private void OnDisable()
    {
        TriggerChange.OnMusicSounded -= PlaySound;
    }

    private void PlaySound(AudioClip clip, float pithchMin, float pitchMax)
    {
        audioSource.Stop();
        audioSource.pitch = UnityEngine.Random.Range(pithchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }
}
