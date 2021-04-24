using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;

    [Space, Header("UI References")]
    public TextMeshProUGUI relicCountText;

    void Start()
    {

    }

    void Update()
    {

    }
}