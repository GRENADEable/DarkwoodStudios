using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeTreeGate : MonoBehaviour
{
    private AudioSource aud;
    public  AudioClip audchop;
    private bool isPlayed;

    void Start()
    {
        isPlayed = false;
        aud = GetComponent<AudioSource>();

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameVariables.Axe > 0 && Input.GetKey(KeyCode.E))
        {
            if (!isPlayed)
            {
                aud.PlayOneShot(audchop);
                isPlayed = true;
            }
                Destroy(gameObject);

        }
    }
}
