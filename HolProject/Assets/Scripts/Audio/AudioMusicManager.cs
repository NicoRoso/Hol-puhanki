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
        
    }

    private void OnDisable()
    {
        
    }

    private void PlaySound(AudioClip clip, float pithchMin, float pitchMax)
    {
        audioSource.pitch = UnityEngine.Random.Range(pithchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }
}
