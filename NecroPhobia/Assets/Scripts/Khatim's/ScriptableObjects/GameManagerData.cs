using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameManagerData", menuName = "Managers/GameManagerData")]

public class GameManagerData : ScriptableObject
{
    [Space, Header("Enums")]
    public PlayerState player = PlayerState.Moving;
    public enum PlayerState { Moving, Examine, Dead };

    void OnEnable()
    {
        player = PlayerState.Moving;
    }
}