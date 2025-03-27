using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public AudioSource AudioSourceComponent;

    [SerializeField] private AudioClip homeMusic;
    [SerializeField] private AudioClip SoundClick;
    [SerializeField] private AudioClip SoundJump;
    [SerializeField] private AudioClip SoundLaser;
    [SerializeField] private AudioClip SoundBombExplosion;
    [SerializeField] private AudioClip SoundMeteoriteExplosion;
    [SerializeField] private AudioClip SoundDash;
    [SerializeField] private AudioClip SoundCollectItem;
    [SerializeField] private AudioClip Congratulations;
    [SerializeField] private AudioClip SoundGameOver;
    [SerializeField] private AudioClip CongratulationsCompleted;
    [SerializeField] private AudioClip BackGround;
    [SerializeField] private AudioClip SoundAppearanceCool;

    public static AudioManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StopCurrentSound()
    {
        if (AudioSourceComponent != null && AudioSourceComponent.isPlaying) 
        {
            AudioSourceComponent.Stop();
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

    public void PlaySoundDash()
    {
        AudioSourceComponent.PlayOneShot(SoundDash);
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

    public void PlaySoundAppearanceCool()
    {
        AudioSourceComponent.PlayOneShot(SoundAppearanceCool);
    }
}
