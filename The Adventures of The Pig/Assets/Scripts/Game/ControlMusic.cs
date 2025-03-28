using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControlMusic : MonoBehaviour
{
    public static ControlMusic Instance { get; private set; }

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        AudioManager.Instance.StopCurrentSound();

        if (scene.name == "Home")
        {
            AudioManager.Instance.PlaySoundhomeMusic();
        }
        else if (scene.name == "LoadingScene")
        {

        }
        else if (scene.name == "Settings")
        {
            AudioManager.Instance.PlaySoundhomeMusic();
        }
        else if (scene.name == "HowtoPlay")
        {
            AudioManager.Instance.PlaySoundhomeMusic();
        }
        else if (scene.name == "End")
        {
            AudioManager.Instance.PlaySoundCongratulationsCompleted();
        }
        else
        {
            AudioManager.Instance.PlaySoundBackGround();
        }
    }
}
    

