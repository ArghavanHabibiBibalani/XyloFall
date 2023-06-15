using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UILevelManager : MonoBehaviour
{
    public void PasueButtonEnter()
    {
        Time.timeScale = 0;
    }

    public void PasueButtonExit()
    {
        Time.timeScale = 1f;
    }

    public void AudioHandler(bool mute)
    {
        if (mute)
        {
            AudioListener.volume = 1;
        }
        else
        {
            AudioListener.volume = 0;
        }
    }

     

    public void BackToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

}
