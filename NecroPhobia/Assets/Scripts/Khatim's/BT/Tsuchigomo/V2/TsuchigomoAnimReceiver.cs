using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TsuchigomoAnimReceiver : MonoBehaviour
{
    public AudioSource audRoarSFX;

    public void OnRoarEvent()
    {
        audRoarSFX.Play();
    }
}