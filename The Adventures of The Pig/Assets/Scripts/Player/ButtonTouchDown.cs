using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ButtonTouchDown : MonoBehaviour, IPointerDownHandler
{
    /*
    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Button Jump")
        {
            PlayerControllers.Singleton.PlayerJump();

        }
        if(gameObject.name == "Button Restart")
        {
            Debug.Log("Game is Restarting");
            PlayerControllers.Singleton.GameRestart();
        }
    }
    */

    public void OnPointerDown(PointerEventData eventData)
    {
        if (gameObject.name == "Button Restart")
        {
            GameOverManager.Instance.GameRestart();
            PlayerPrefs.DeleteKey("PlayerHP");
            Debug.Log("Game is Restarting");
        }

        if (gameObject.name == "Button Home")
        {
            SceneManager.LoadScene("Home");
        }
    }

    public void ButtonRestart()
    {
        GameOverManager.Instance.GameRestart();
        PlayerPrefs.DeleteKey("PlayerHP");
        Debug.Log("Game is Restarting");
    }

    public void ButtonHome()
    {
        SceneManager.LoadScene("Home");
    }
}
