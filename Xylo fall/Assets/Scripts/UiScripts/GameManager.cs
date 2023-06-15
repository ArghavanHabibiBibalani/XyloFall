using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action OnSceneChanged;

    [HideInInspector]
    public GameStateType CurrentStateType = GameStateType.MAINMENU;

    public Button[] levelButtons;

    private int CurrentLevel;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        CurrentLevel = 1;
    }

    private void Start()
    {
        // Attach the OnClick method to each level button
        for (int i = 1; i <= levelButtons.Length; i++)
        {
            int levelIndex = i;

            levelButtons[i - 1].onClick.AddListener(() => LoadLevel(levelIndex));
        }
    }

    private void LoadLevel(int levelIndex)
    {
        if (CurrentLevel >= levelIndex)
        {
            string sceneName = "Level" + levelIndex;
            CurrentStateType = GameStateType.LEVEL;
            OnSceneChanged.Invoke();

            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }

    public void StartButton()
    {
        AudioManager.instance.PlaySoundOneShot("barA", 1);
        LoadLevel(CurrentLevel);
    }

    public void LevelButton()
    {
        AudioManager.instance.PlaySoundOneShot("barC", 1);
    }
    public void CreditsButton()
    {
        AudioManager.instance.PlaySoundOneShot("barF", 1);
    }

    public void ExitButton()
    {
        AudioManager.instance.PlaySoundOneShot("barE", 1);
        Application.Quit();
    }

    public enum GameStateType
    {
        MAINMENU, LEVEL
    }
}
