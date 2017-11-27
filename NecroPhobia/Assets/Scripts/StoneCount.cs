using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneCount : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameVariables.Stones += 1;
            Destroy(gameObject);
        }
    }

    void Start ()
    {
		
	}
	
	
	void Update ()
    {
		
	}
}
