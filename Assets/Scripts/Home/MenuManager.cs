using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    public GameObject SelectIcon;
    public List<GameObject> MenuList;

    int MenuIndex = 0;

    public string MenuName = "Menu-StartGame";
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectPreviousMenu();
            AudioManager.instance.PlaySoundClick();
        } 
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNextMenu();
            AudioManager.instance.PlaySoundClick(); 
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (MenuName == "Menu-StartGame")
            {
                ApplicationVariables.LoadingScenename = "Level1";
                SceneManager.LoadScene("LoadingScene");
            }
            if (MenuName == "Menu-Settings")
            {
                ApplicationVariables.LoadingScenename = "Settings";
                SceneManager.LoadScene("LoadingScene");
            }
            if (MenuName == "Menu-Exit")
            {
                Application.Quit();
            }
        }
    }

    void SelectNextMenu()
    {
        MenuIndex++;
        if (MenuIndex >= 3)
        {
            MenuIndex = 0;
        }
        GameObject SelectingMenu = MenuList[MenuIndex];
        SelectIcon.transform.position = new Vector2(SelectIcon.transform.position.x, SelectingMenu.transform.position.y);
        MenuName = SelectingMenu.name;
    }
    void SelectPreviousMenu()
    {
        MenuIndex--;
        if (MenuIndex <0)
        {
            MenuIndex = MenuList.Count - 1;
        }
        GameObject SelectingMenu = MenuList[MenuIndex];
        SelectIcon.transform.position = new Vector2(SelectIcon.transform.position.x, SelectingMenu.transform.position.y);
        MenuName = SelectingMenu.name;
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
        if (scene.name == "Home")
        {
            AudioManager.instance.PlaySoundhomeMusic();
        }
    }
}
