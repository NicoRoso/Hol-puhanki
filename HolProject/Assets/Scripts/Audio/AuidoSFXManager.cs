using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AuidoSFXMangaer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        Movement.OnMovedSounded += PlayRandomClipSound;
        Movement.OnSounded += PlaySound;
        PlayerDash.OnDashedSounded += PlaySound;
        Gate.OnClosed += Play;
    }

    private void OnDisable()
    {
        Movement.OnMovedSounded -= PlayRandomClipSound;
        Movement.OnSounded -= PlaySound;
        PlayerDash.OnDashedSounded -= PlaySound;
        Gate.OnClosed -= Play;
    }

    private void PlaySound(AudioClip clip, float pithchMin, float pitchMax)
    {
        audioSource.pitch = UnityEngine.Random.Range(pithchMin, pitchMax);
        audioSource.PlayOneShot(clip);
    }
    private void PlayRandomClipSound(AudioClip[] clip, float pithchMin, float pitchMax)
    {
        audioSource.pitch = UnityEngine.Random.Range(pithchMin, pitchMax);
        int index = Random.Range(0, clip.Length);
        audioSource.PlayOneShot(clip[index]);
    }

    private void Play(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
}
