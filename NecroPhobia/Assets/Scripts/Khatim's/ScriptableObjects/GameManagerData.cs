using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [Space, Header("Enums")]
    public PlayerState player = PlayerState.Moving;
    public enum PlayerState { Idle, Moving, Examine, Dead };

    public MenuState menu = MenuState.MainMenu;
    public enum MenuState { MainMenu, Game, Pause, Death };

    [Space, Header("Player Movement")]
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float gravity = -9.81f;

    [Space, Header("Player Stamina")]
    public float currStamina;
    public float regenStamina;
    public float depleteStamina;
    public float maxStamina;

    [Space, Header("Pickup Items")]
    public float relicCount;
    public float axeCount;

    public delegate void SendEvents();
    public static event SendEvents onGameStart;

    void OnEnable()
    {
        //menu = MenuState.MainMenu;
        player = PlayerState.Moving;
    }

    public void Start()
    {
        if (onGameStart != null)
        {
            onGameStart();
            Debug.Log("Game Started");
        }
    }
}