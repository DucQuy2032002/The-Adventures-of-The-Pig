using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public static GameOverManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayerDied()
    {
        //When player dies, save the name of the CurrentLevel
        PlayerPrefs.SetString("CurrentLevel", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();
        Debug.Log("PlayerDied has been active");
    }

    public void GameRestart()
    {
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            //Get the Level name from the PlayerPrefs
            string currentLevel = PlayerPrefs.GetString("CurrentLevel");

            //Reload that level
            SceneManager.LoadScene(currentLevel);
            Debug.Log("Returns the saved scene");
        }
        else
        {
            SceneManager.LoadScene("Level1");
            Debug.Log("No scene saved, return scene level 1");
        }
    }
}
