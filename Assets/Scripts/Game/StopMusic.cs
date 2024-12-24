using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StopMusic : MonoBehaviour
{   
    private void Awake()
    {
        var AudioManagerComponent = FindAnyObjectByType<AudioManager>();
        string currentSceneName = SceneManager.GetActiveScene().name;
        
        if (AudioManagerComponent != null)
        {
            if (currentSceneName == "LoadingScene")
            {
                AudioManagerComponent.PauseMusic();
            }
            else
            {
                AudioManagerComponent.ResumeMusic(); 
            }
        }
        else
        {
            Debug.LogError("AudioManagerComponent is null. Ensure AudioManager exists in the scene.");
        }

        
    }
}
    

