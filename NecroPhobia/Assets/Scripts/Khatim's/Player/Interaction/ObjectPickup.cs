using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPickup : PlayerInteraction
{
    public GameObject pickedObj;

    public override void StartInteraction()
    {
        base.StartInteraction();
        pickedObj = interactCol.gameObject;
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
