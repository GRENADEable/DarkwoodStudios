using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeTreeGate : MonoBehaviour
{
    private AudioSource aud;
    public  AudioClip audchop;
    public GameObject tree;


    void Start()
    {
        aud = GetComponent<AudioSource>();

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameVariables.Axe > 0 && Input.GetKey(KeyCode.E))
        {
            aud.PlayOneShot(audchop);
            Destroy(tree);
            GameVariables.Axe -= 1;

        }
    }
}
