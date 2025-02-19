using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalSettings
{
    public static Language language = Language.Russian;
    public static OnLanguageChanged onLanguageChanged;
    public static OnMusicVolumeChanged onMusicVolumeChanged;
    public static OnSoundVolumeChanged onSoundVolumeChanged;
    private static float _soundVolume = 0.3f;
    private static float _musicVolume = 0.3f;
    public static float SoundVolume { private set {} get { return _soundVolume; } }
    public static float MusicVolume { private set {} get { return _musicVolume; } }

    public static void ChangeSoundVolume(float volume)
    {
        if (volume <= 0f)
            _soundVolume = 0f;
        else if (volume > 1f)
            _soundVolume = 1f;
        else
            _soundVolume = volume;
        onSoundVolumeChanged?.Invoke();
    } 

    public static void ChangeMusicVolume(float volume)
    {
        if (volume <= 0f)
            _musicVolume = 0f;
        else if (volume > 1f)
            _musicVolume = 1f;
        else
            _musicVolume = volume;
        onMusicVolumeChanged?.Invoke();
    }
}

public enum Language
{
    English = 0,
    Russian = 1
}

public enum AudioType
{
    Sound = 0,
    Music = 1
}

public delegate void OnLanguageChanged();
public delegate void OnMusicVolumeChanged();
public delegate void OnSoundVolumeChanged();

