using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Collider interactCol;

    public virtual void StartInteraction()
    {
        //Debug.Log("Interaction Started");
    }

    public virtual void UpdateInteraction()
    {
        //Debug.Log("Interaction Running");
    }

    public virtual void EndInteraction()
    {
        //Debug.Log("Interaction Ending");
    }
}