using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Public Variables

    [Space, Header("Data")]
    public GameManagerData gmData;

    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;

    [Space, Header("UI References")]
    public TextMeshProUGUI relicCountText;
    #endregion

    #region Private Variables
    private int _currRelicCount = 0;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        ExamineSystem.OnRelicDestroy += OnRelicDestroyEventReceived;
    }

    void OnDisable()
    {
        ExamineSystem.OnRelicDestroy -= OnRelicDestroyEventReceived;
    }

    void OnDestroy()
    {
        ExamineSystem.OnRelicDestroy -= OnRelicDestroyEventReceived;
    }
    #endregion


    void Start()
    {
        DisableCursor();
    }

    void Update()
    {

    }
    #endregion

    #region My Functions

    #region Cursor
    void EnableCursor()
    {
        gmData.VisibleCursor(true);
        gmData.LockCursor(false);
    }

    void DisableCursor()
    {
        gmData.VisibleCursor(false);
        gmData.LockCursor(true);
    }
    #endregion

    #endregion

    #region Events
    void OnRelicDestroyEventReceived()
    {
        _currRelicCount++;
        relicCountText.text = $"Relic Count: {_currRelicCount}";
    }
    #endregion
}