using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] _clips;
    private AudioSource _audioSource;
    private uint _currentClipIndex = 0;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = _clips[0];
        _audioSource.Play();
    }
    public void PlayNextClip()
    {
        _currentClipIndex++;

        if (_currentClipIndex < _clips.Length)
        {
            _audioSource.clip = _clips[_currentClipIndex];
            _audioSource.Play();
        }
    }

    public void PlayClip(uint clipIndex)
    {
        if (clipIndex < _clips.Length)
        {
            _currentClipIndex = clipIndex;
            _audioSource.clip = _clips[_currentClipIndex];
            _audioSource.Play();
        }
    }

    public void Pause()
    {
        _audioSource.Pause();
    }
}
