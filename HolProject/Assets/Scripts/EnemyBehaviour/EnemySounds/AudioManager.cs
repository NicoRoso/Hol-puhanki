using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    private void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.dopplerLevel = 0;
            s.source.spatialBlend = s.spatialBlend;
            s.source.playOnAwake = false;
        }
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
    public void PlayWithDelay(string name, float delay)
    {
        StartCoroutine(WaiterForDelay(name, delay));
    }
    public void PlayOther(AudioSource audioSource)
    {
        audioSource.Play();
    }
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Stop();
    }
    public void SoftStop(string name, float softness)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        StartCoroutine(SoftStopCoroutine(s, softness));
    }
    IEnumerator SoftStopCoroutine(Sound s, float softness)
    {
        while (s.source.volume > 0)
        {
            s.source.volume -= 1 / softness;
            yield return null;
        }
        s.source.Stop();
        s.source.volume = s.volume;
        yield break;
    }
    IEnumerator WaiterForDelay(string name, float delay)
    {
        yield return new WaitForSeconds(delay);
        Play(name);
        yield break;
    }
}
