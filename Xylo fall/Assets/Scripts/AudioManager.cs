using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] Sounds;

    private void Awake()
    {
        foreach (Sound sound in Sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
        }
    }
    public void PlaySound(string name, float volume, float pitch)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        sound.Source.volume = volume;
        sound.Source.pitch = pitch;
        sound.Source.Play();
    }
    public void PlaySound(int index, float volume, float pitch)
    {
        Sound sound = Sounds[index];
        sound.Source.volume = volume;
        sound.Source.pitch = pitch;
        sound.Source.Play();
    }
}
