using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeTreeGate : MonoBehaviour
{
    private AudioSource aud;
    public  AudioClip audchop;
    public GameObject tree;
    public GameObject hatchetIcon;


    void Start()
    {
        aud = GetComponent<AudioSource>();

    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameVariables.Axe > 0 && Input.GetKey(KeyCode.E))
        {
            hatchetIcon.SetActive(false);
            aud.PlayOneShot(audchop, 0.5f);
            Destroy(tree);
            GameVariables.Axe -= 1;

        }
    }
}
