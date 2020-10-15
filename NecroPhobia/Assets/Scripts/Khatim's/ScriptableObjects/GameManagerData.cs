﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]
public class GameManagerData : ScriptableObject
{
    [Space, Header("Enums")]
    public PlayerState player = PlayerState.Moving;
    public enum PlayerState { Moving, Examine, Dead };

    [Space, Header("Player Movement")]
    public float currSpeed;
    public float playerWalkSpeed;
    public float playerRunSpeed;
    public float gravity = -9.81f;

    [Space, Header("Player Stamina")]
    public float currStamina;
    public float regenStamina;
    public float depleteStamina;
    public float maxStamina;

    void OnEnable()
    {
        player = PlayerState.Moving;
    }
}