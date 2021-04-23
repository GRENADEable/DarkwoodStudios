using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public GameObject pickedObj;

    public delegate void SendEvents(GameObject obj);
    public static event SendEvents onObjPickup;

    void OnEnable()
    {
        UIManager.onExamineExit += OnExamineExitEventReceived;
    }

    void OnDisable()
    {
        UIManager.onExamineExit -= OnExamineExitEventReceived;
    }

    void OnDestroy()
    {
        UIManager.onExamineExit -= OnExamineExitEventReceived;
    }

    public override void StartInteraction()
    {
        base.StartInteraction();
        pickedObj = interactCol.gameObject;

        onObjPickup?.Invoke(pickedObj); // Events sent to UI Manager script
        Debug.Log("Event Sent");
    }

    public override void UpdateInteraction()
    {
        base.UpdateInteraction();
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
    }

    void OnExamineExitEventReceived()
    {
        interactCol = null;
        pickedObj = null;
    }
}
