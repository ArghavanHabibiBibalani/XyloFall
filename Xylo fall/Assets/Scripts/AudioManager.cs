using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public Sound[] Sounds;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            foreach (Sound sound in Sounds)
            {
                sound.Source = gameObject.AddComponent<AudioSource>();
                sound.Source.clip = sound.Clip;
            }
        }
    }
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") { PlaySound("menuMusic", 0.6f); }
        else { PlaySound("levelMusic", 0.6f); }
    }
    public void PlaySound(string name, float volume)
    {
        Sound sound = Array.Find(Sounds, s => s.Name == name);
        sound.Source.volume = volume;
        sound.Source.Play();
    }
    public void PlaySound(int index, float volume)
    {
        Sound sound = Sounds[index];
        sound.Source.volume = volume;
        sound.Source.Play();
    }
    public void PlaySoundOneShot(string name, float volume)
    {
        AudioSource source = Array.Find(Sounds, s => s.Name == name).Source;
        source.PlayOneShot(source.clip, volume);
    }
    public void PlaySoundOneShot(int index, float volume)
    {
        AudioSource source = Sounds[index].Source;
        source.PlayOneShot(source.clip, volume);
    }
}
