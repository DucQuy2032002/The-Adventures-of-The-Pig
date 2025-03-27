using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI loadingText; //Loading line
    [SerializeField] private TextMeshProUGUI loadingTextAmount; //Load percentage display line

    [SerializeField] private Slider loadingSlider; //progress bar

    private void Start()
    {
        StartCoroutine(LoadYourAsyncScene());

    }

    IEnumerator LoadYourAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(ApplicationVariables.LoadingScenename);
        Debug.Log("Scene is: " + ApplicationVariables.LoadingScenename);
        asyncLoad.allowSceneActivation = false; //after scene is loaded, don't active it

        while (asyncLoad.progress < 0.9f) //asyncLoad.progress max is 0.9
        {
            float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f);
            loadingSlider.value = progress;
            loadingTextAmount.text = " " + Mathf.RoundToInt(asyncLoad.progress * 100);
            yield return null; //keep waiting if asyncLoad.isDone = false
        }
        loadingSlider.value = 1f;
        loadingTextAmount.text = "100%";

        yield return new WaitForSeconds(1);
        asyncLoad.allowSceneActivation = true;
    }
}
