using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public GameObject pickedObj;

    public delegate void SendEvents(GameObject obj);
    public static event SendEvents onObjPickup;

    public override void StartInteraction()
    {
        base.StartInteraction();
        pickedObj = interactCol.gameObject;

        if (onObjPickup != null) // Events sent to UI Manager script
            onObjPickup(pickedObj);
    }

    public override void UpdateInteraction()
    {
        base.UpdateInteraction();
    }

    public override void EndInteraction()
    {
        base.EndInteraction();
    }
}
