using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioTools
{
    public static void PlaySound(AudioSource audioSource)
    {
        if (audioSource != null)
            if (!audioSource.isPlaying)
                audioSource.Play();     
    }
    public static void PlaySound(AudioSource audioSource, AudioClip clip)
    {
        if (audioSource != null)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = clip;
                audioSource.Play();
            }
        }
    }
    public static IEnumerator PlayLoopSound(AudioSource audioSource, float delay)
    {
        while (audioSource != null)
        {
            yield return new WaitForSeconds(delay);
            audioSource.Play();
        }   
    }
    public static IEnumerator PlayLoopSound(AudioSource audioSource, float minDelay, float maxDelay)
    {
        float time = Random.Range(minDelay, maxDelay);
        while (audioSource != null)
        {
            yield return new WaitForSeconds(time);
            audioSource.Play();
        }   
    }

    public static IEnumerator PlayLoopSound(AudioSource audioSource, AudioClip clip, float minDelay, float maxDelay)
    {
        float time = Random.Range(minDelay, maxDelay);
        while (audioSource != null)
        {
            yield return new WaitForSeconds(time);
            audioSource.clip = clip;
            audioSource.Play();
        }   
    }
}
