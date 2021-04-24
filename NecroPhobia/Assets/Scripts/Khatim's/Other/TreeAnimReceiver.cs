using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAnimReceiver : MonoBehaviour
{
    public void OnTreeFall()
    {
        Destroy(gameObject);
    }
}