using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("Audio Effects")]
    public AudioSource sfxSource;
    public AudioSource mainMenuOST;
    public AudioSource ambientSfx;
    //public AudioSource deathScreenOST;
    public AudioClip[] soundClips;

    void OnEnable()
    {

    }
    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }

    void Awake()
    {
        //DontDestroyOnLoad(this);
        MenuState();
    }

    void AudioAcess(int index)
    {
        sfxSource.PlayOneShot(soundClips[index]);
    }

    void MenuState()
    {
        switch (gameManagerData.currMenuState)
        {
            case GameManagerData.MenuState.MainMenu:
                mainMenuOST.Play();
                break;

            case GameManagerData.MenuState.Game:
                ambientSfx.Play();
                break;

            case GameManagerData.MenuState.Death:
                break;

            case GameManagerData.MenuState.Pause:
                break;

            default:
                break;
        }
    }

    void StopAudio()
    {
        sfxSource.Stop();
        mainMenuOST.Stop();
        ambientSfx.Stop();
    }
}