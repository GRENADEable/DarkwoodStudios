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
        if (play.tag == "Player" && ply.GetComponent<PlayerController>().invisTimer <= 0)
        {
            aud.PlayOneShot(growlClip, 0.045f);
            aud.PlayOneShot(chaseClip, 0.045f);
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
