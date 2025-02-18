using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateVolume : MonoBehaviour
{
    [SerializeField] private AudioType _audioType;
    private AudioSource _audioSource;
    

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        if (_audioType == AudioType.Sound)
        {
            GlobalSettings.onSoundVolumeChanged += ChangeSoundVolume;
            ChangeSoundVolume();
        }    
        else
        {
            GlobalSettings.onMusicVolumeChanged += ChangeMusicVolume;
            ChangeMusicVolume();
        }     
    }

    private void OnDisable()
    {
        if (_audioType == AudioType.Sound)
            GlobalSettings.onSoundVolumeChanged -= ChangeSoundVolume;
        else
            GlobalSettings.onMusicVolumeChanged -= ChangeMusicVolume;
    }

    private void ChangeSoundVolume()
    {
        _audioSource.volume = GlobalSettings.SoundVolume;
    }

    private void ChangeMusicVolume()
    {
        _audioSource.volume = GlobalSettings.MusicVolume;
    }
}
