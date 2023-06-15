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
                if (sound.Name == "levelMusic" ||  sound.Name == "menuMusic") { sound.Source.loop = true; }
            }
        }
    }
    private void Start()
    {
        GameManager.OnSceneChanged += OnSceneChanged;
        UpdateMusic();
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
    private void OnSceneChanged()
    {
        UpdateMusic();
    }
    private void UpdateMusic()
    {
        foreach (Sound s in Sounds)
        {
            if (s.Name == "levelMusic" || s.Name == "menuMusic") { s.Source.Stop(); }
        }
        if (GameManager.instance.CurrentStateType == GameManager.GameStateType.MAINMENU) { PlaySound("menuMusic", 0.6f); }
        else { PlaySound("levelMusic", 0.6f); }
    }
}
