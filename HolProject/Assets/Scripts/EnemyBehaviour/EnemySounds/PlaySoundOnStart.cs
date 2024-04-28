using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] string _soundName;
    private void Start()
    {
        GetComponent<AudioManager>().Play(_soundName);
    }
}
