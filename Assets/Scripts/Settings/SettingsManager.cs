using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SettingsManager : MonoBehaviour
{
    public string SettingsName = "Back";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (SettingsName == "Back")
            {
                ApplicationVariables.LoadingScenename = "Home";
                SceneManager.LoadScene("LoadingScene");
            }
        }
    }

}
