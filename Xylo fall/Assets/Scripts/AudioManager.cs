using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public void PlaySound(string name, float volume, float pitch)
    {
        Sound sound = Array.Find(sounds, s => s.Name == name);
        sound.Source.volume = volume;
        sound.Source.pitch = pitch;
        sound.Source.Play();
    }
    private void Awake()
    {
        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
        }
    }
}
