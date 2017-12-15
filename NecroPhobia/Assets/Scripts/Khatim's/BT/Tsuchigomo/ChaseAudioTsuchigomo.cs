using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAudioTsuchigomo : MonoBehaviour
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
            aud.PlayOneShot(growlClip, 0.045f);
            aud.PlayOneShot(chaseClip, 0.045f);
        }

    }
}
