using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class EndPointManager : MonoBehaviour
{
    Animator AnimatorComponent;
    public ParticleSystem FireWork;

    [SerializeField] private string nextSceneName;

    private void Awake()
    {
        AnimatorComponent = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            AnimatorComponent.Play("CupPoint");
            CreateFireWork();
            CreateCongratulations();
            StartCoroutine(TransferScene());
        }
    }

    void CreateFireWork()
    {
        FireWork.Play();

    }

    void CreateCongratulations()
    {
        AudioManager.instance.PlaySoundCongratulations();
    }

    IEnumerator TransferScene()
    {
        yield return new WaitForSeconds(1.5f);
        ApplicationVariables.LoadingScenename = nextSceneName;
        SceneManager.LoadScene("LoadingScene");
    }
}
