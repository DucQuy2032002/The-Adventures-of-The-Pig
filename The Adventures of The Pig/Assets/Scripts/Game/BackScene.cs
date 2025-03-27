using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackScene : MonoBehaviour
{
    public string Back = "Back";

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (Back == "Back")
            {
                ApplicationVariables.LoadingScenename = ApplicationVariables.PreviousSceneName;
                SceneManager.LoadScene("LoadingScene");
            }
        }
    }

    public void ButtonBack()
    {
        ApplicationVariables.LoadingScenename = ApplicationVariables.PreviousSceneName;
        SceneManager.LoadScene("LoadingScene");
    }

}
