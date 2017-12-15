using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DocumentPickup : MonoBehaviour
{
    public GameObject docScreen;
    public Collider col;
    public GameObject relicPickupText;

    private AudioSource aud;
    public AudioClip audPaper;

    // Use this for initialization
    void Start ()
    {
        docScreen.SetActive(false);
        relicPickupText.SetActive(false);

        aud = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (col != null && Input.GetKeyDown(KeyCode.E))
        {
            docScreen.SetActive(!docScreen.activeSelf);
            if (docScreen.activeSelf)
            {
                aud.PlayOneShot(audPaper, 0.15f);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            col = other;
            relicPickupText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            col = null;
            docScreen.SetActive(false);
            relicPickupText.SetActive(false);
        }
    }
}
