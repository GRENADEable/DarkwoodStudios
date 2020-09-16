using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;

    void OnEnable()
    {
        PlayerControllerV2.onRelicTriggerEnter += OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit += OnRelicTriggerExitEventReceived;
    }

    void OnDisable()
    {
        PlayerControllerV2.onRelicTriggerEnter -= OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit -= OnRelicTriggerExitEventReceived;
    }

    void OnDestroy()
    {
        PlayerControllerV2.onRelicTriggerEnter -= OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit -= OnRelicTriggerExitEventReceived;
    }

    void Start()
    {
    }

    void Update()
    {

    }

    void OnRelicTriggerEnterEventReceived()
    {
        relicPickupTxt.SetActive(true);
    }

    void OnRelicTriggerExitEventReceived()
    {
        relicPickupTxt.SetActive(false);
    }
}