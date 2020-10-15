using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("Player")]
    public GameObject player;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;
        gameManagerData.currStamina = gameManagerData.maxStamina;
    }

    void Update()
    {
        gameManagerData.currStamina = Mathf.Clamp(gameManagerData.currStamina, 0, gameManagerData.maxStamina);
    }
}