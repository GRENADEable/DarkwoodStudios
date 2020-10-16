using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SceneManage : MonoBehaviour
{

    void OnEnable()
    {
        GameManagerData.onGameStart += OnGameStartEventReceived;
    }

    void OnDisable()
    {
        GameManagerData.onGameStart -= OnGameStartEventReceived;
    }

    void OnDestroy()
    {
        GameManagerData.onGameStart -= OnGameStartEventReceived;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("GameV2");
    }

    public void Exit()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void OnGameStartEventReceived()
    {
        SceneManager.LoadScene("GameV2");
    }
}
