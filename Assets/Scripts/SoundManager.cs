using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource audioSource;
    public static SoundManager instance;
    private void Awake()
    {
        if (instance != null)
            Destroy(instance);
        else
            instance = this;
    }
    private void Start()
    {
        audioSource = GetComponents<AudioSource>()[0];
    }
    public enum Sound
    {
        Jump,
        PowerUp,
        Dead,
        GameOver,
        BreakBlock,
        Stomp
    }
    public SoundAudioClip[] soundAudioClipArray;
    [System.Serializable]
    public class SoundAudioClip
    {
        public Sound sound;
        public AudioClip audioClip;
    }
    public void PlaySound(Sound sound)
    {
        foreach (var soundAudioClip in soundAudioClipArray)
        {
            if (soundAudioClip.sound == sound)
            {
                audioSource.PlayOneShot(soundAudioClip.audioClip);
                return;
            }
        }
        Debug.Log("Sound not found");
    }
}
