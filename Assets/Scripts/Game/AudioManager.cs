using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource AudioSourceComponent;

    [SerializeField] private AudioClip homeMusic;
    [SerializeField] private AudioClip SoundClick;
    [SerializeField] private AudioClip SoundJump;
    [SerializeField] private AudioClip SoundLaser;
    [SerializeField] private AudioClip SoundBombExplosion;
    [SerializeField] private AudioClip SoundMeteoriteExplosion;
    [SerializeField] private AudioClip SoundCollectItem;
    [SerializeField] private AudioClip Congratulations;
    [SerializeField] private AudioClip SoundGameOver;
    [SerializeField] private AudioClip CongratulationsCompleted;
    [SerializeField] private AudioClip BackGround;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void PauseMusic()
    {
        if (AudioSourceComponent != null && AudioSourceComponent.isPlaying) 
        {
            AudioSourceComponent.Pause();
        }
    }

    public void ResumeMusic()
    {
        if (AudioSourceComponent != null)
        {
            AudioSourceComponent.UnPause();
        }
    }

    public void PlaySoundhomeMusic()
    {
        AudioSourceComponent.PlayOneShot(homeMusic);
    }

    public void PlaySoundClick()
    {
        AudioSourceComponent.PlayOneShot(SoundClick);
    }

    public void PlaySoundJump() 
    {
        AudioSourceComponent.PlayOneShot(SoundJump);
    }

    public void PlaySoundLaser()
    {
        AudioSourceComponent.PlayOneShot(SoundLaser);
    }

    public void PlaySoundBombExplosion()
    {
        AudioSourceComponent.PlayOneShot(SoundBombExplosion);
    }
    
    public void PlaySounddMeteoriteExplosion()
    {
        AudioSourceComponent.PlayOneShot(SoundMeteoriteExplosion);
    }

    public void PlaySoundCollectItem()
    {
        AudioSourceComponent.PlayOneShot(SoundCollectItem);
    }

    public void PlaySoundCongratulations()
    {
        AudioSourceComponent.PlayOneShot(Congratulations);
    }

    public void PlaySoundGameOver()
    {
        AudioSourceComponent.PlayOneShot(SoundGameOver);
    }

    public void PlaySoundCongratulationsCompleted()
    {
        AudioSourceComponent.PlayOneShot(CongratulationsCompleted);
    }
    public void PlaySoundBackGround()
    {
        AudioSourceComponent.PlayOneShot(BackGround);
    }
}
