using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider sfxVolume, musicVolume, voiceVolume;

    [SerializeField] private GameObject _menuPanel;

    private void Awake()
    {
        this.gameObject.SetActive(false);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", volume);
    }
    
    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("Music", volume);
    }

    public void SetVoiceVolume(float volume)
    {
        _audioMixer.SetFloat("Voice", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }

    public void BackToMenus()
    {
        _menuPanel.SetActive(true);
        this.gameObject.SetActive(false);
    }
}
