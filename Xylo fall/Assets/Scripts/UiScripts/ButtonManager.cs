using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public AudioClip[] Sounds;
    private AudioSource audioSource;
    public string currentLevel;
    public Button[] levelButtons;
    private void Awake()
    {
        audioSource = transform.GetComponent<AudioSource>();
        // Attach the OnClick method to each level button
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i ; // Assuming level indices start from 1

            levelButtons[i].onClick.AddListener(() => LevelOpen(levelIndex));
        }
    }

    private void LevelOpen(int levelIndex)
    {
        // Assuming your scene names follow a specific pattern, e.g., "Level1", "Level2", etc.
        string sceneName = "Level" + levelIndex;

        // Load the specified scene
        SceneManager.LoadScene(sceneName);
    }


    public void StartButton()
    {
        audioSource.PlayOneShot(Sounds[0]);
        
        SceneManager.LoadScene(currentLevel);
    }

    public void LevelButton()
    {
        audioSource.PlayOneShot(Sounds[1]);
    }
    public void CreditsButton()
    {
        audioSource.PlayOneShot(Sounds[2]);
    }

    public void ExitButton()
    {
        audioSource.PlayOneShot(Sounds[3]);
        Application.Quit();
    }




    
}
