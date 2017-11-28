using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManage : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("GameV2");
    }

    public void Extras()
    {
        SceneManager.LoadScene("Extras");
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
