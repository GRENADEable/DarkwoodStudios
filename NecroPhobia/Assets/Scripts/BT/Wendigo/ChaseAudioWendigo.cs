using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAudioWendigo : MonoBehaviour
{
    public AudioClip growlClip;
    public AudioClip chaseClip;

    private AudioSource aud;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider play)
    {
        if (play.gameObject.tag == "Player")
        {
            aud.PlayOneShot(growlClip);
            aud.PlayOneShot(chaseClip);
        }

    }
    private void OnTriggerExit(Collider stop)
    {
        if (stop.gameObject.tag == "Player")
        {
            aud.Stop();
        }
    }
}
