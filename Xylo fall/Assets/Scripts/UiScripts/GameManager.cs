using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static event Action OnSceneChanged;

    [HideInInspector]
    public bool ChangeLevelToMenu = false;
    [HideInInspector]
    public bool ProgressLevel = false;
    [HideInInspector]
    public GameStateType CurrentStateType = GameStateType.MAINMENU;

    public Button[] levelButtons;

    private int HighestLevel;

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
        HighestLevel = 1;
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

    public void Update()
    {
        if (ChangeLevelToMenu)
        {
            ChangeLevelToMenu = false;
            CurrentStateType = GameStateType.MAINMENU;
            OnSceneChanged.Invoke();
            SceneManager.LoadScene("MainMenu");
        }
        if (ProgressLevel)
        {
            ProgressLevel = false;
            var currentLevel = SceneManager.GetActiveScene().buildIndex;
            if (currentLevel >= HighestLevel) { HighestLevel++; }
            if (currentLevel < SceneManager.sceneCountInBuildSettings - 1) { LoadLevel(currentLevel + 1); }
            else 
            { 
                SceneManager.LoadScene("MainMenu");
                gameObject.SetActive(true);
                CurrentStateType = GameStateType.MAINMENU;
                OnSceneChanged.Invoke();
            }
            OnSceneChanged.Invoke();
        }
    }

    private void LoadLevel(int levelIndex)
    {
        if (HighestLevel >= levelIndex)
        {
            string sceneName = "Level" + levelIndex;
            CurrentStateType = GameStateType.LEVEL;
            OnSceneChanged.Invoke();
            gameObject.SetActive(false);

            // Load the specified scene
            SceneManager.LoadScene(sceneName);
        }
    }

    public void StartButton()
    {
        AudioManager.instance.PlaySoundOneShot("barA", 1);
        LoadLevel(HighestLevel);
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
