using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public void ButtonStartGame()
    {
        ApplicationVariables.PreviousSceneName = SceneManager.GetActiveScene().name;
        ApplicationVariables.LoadingScenename = "LevelMenu";
        SceneManager.LoadScene("LoadingScene");
        //PlayerPrefs.DeleteKey("ReachedIndex");
        //PlayerPrefs.DeleteKey("UnlockedLevel");
    }

    public void ButtonSettings()
    {
        ApplicationVariables.PreviousSceneName = SceneManager.GetActiveScene().name;
        ApplicationVariables.LoadingScenename = "Settings";
        SceneManager.LoadScene("LoadingScene");
    }

    public void ButtonHowToPlay()
    {
        ApplicationVariables.PreviousSceneName = SceneManager.GetActiveScene().name;
        ApplicationVariables.LoadingScenename = "HowtoPlay";
        SceneManager.LoadScene("LoadingScene");
    }

    public void ButtonExit()
    {
        ApplicationVariables.PreviousSceneName = SceneManager.GetActiveScene().name;
        Application.Quit();
    }

    
}
