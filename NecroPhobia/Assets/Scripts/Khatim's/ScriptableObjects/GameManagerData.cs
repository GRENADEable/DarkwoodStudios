using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [Space, Header("Enums")]
    public PlayerState currPlayerState = PlayerState.Moving;
    public enum PlayerState { Idle, Moving, Examine, Dead };

    public MenuState currMenuState = MenuState.MainMenu;
    public enum MenuState { MainMenu, Game, Pause, Death };

    [Space, Header("Pickup Items")]
    public float relicCount;
    public float axeCount;

    void OnEnable()
    {
        //menu = MenuState.MainMenu;
        currPlayerState = PlayerState.Moving;
    }
    #region My Functions

    #region Game States
    public void LockCursor(bool isLocked)
    {
        if (isLocked)
            Cursor.lockState = CursorLockMode.Locked;
        else
            Cursor.lockState = CursorLockMode.None;
    }

    public void VisibleCursor(bool isVisible)
    {
        if (isVisible)
            Cursor.visible = true;
        else
            Cursor.visible = false;
    }

    public void TogglePause(bool isPaused)
    {
        if (isPaused)
            Time.timeScale = 0f;
        else
            Time.timeScale = 1f;
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
    #endregion

    #endregion
}