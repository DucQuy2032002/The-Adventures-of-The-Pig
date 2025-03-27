using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPointManager : MonoBehaviour
{
    Animator AnimatorComponent;
    public ParticleSystem FireWork;

    [SerializeField] private GameObject DialogVictory;

    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
        DialogVictory.SetActive(false);

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AnimatorComponent.Play("CupPoint");
            CreateFireWork();
            CreateCongratulations();
            UnlockNewLevel();
            FindObjectOfType<TimeManager>().StopTime();
            DialogVictory.SetActive(true);
        }
    }

    void CreateFireWork()
    {
        FireWork.Play();
    }

    void CreateCongratulations()
    {
        AudioManager.Instance.PlaySoundCongratulations();
    }

    void UnlockNewLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int reachedIndex = PlayerPrefs.GetInt("ReachedIndex");

        Debug.Log($"Current Scene Index: {currentSceneIndex}, Reached Index: {reachedIndex}");

        if (currentSceneIndex >= reachedIndex)
        {
            PlayerPrefs.SetInt("ReachedIndex", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnlockedLevel", 1) + 1);
            PlayerPrefs.Save();
        }
    }
}
