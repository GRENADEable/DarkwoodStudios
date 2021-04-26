using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAudioWendigo : MonoBehaviour
{
    public AudioClip growlClip;
    public AudioClip chaseClip;

    private AudioSource aud;
    public GameObject ply;

    void Awake()
    {
        aud = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider play)
    {
        if (play.tag == "Player")
        {
            aud.PlayOneShot(growlClip, 1f);
            aud.PlayOneShot(chaseClip, 0.75f);
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
