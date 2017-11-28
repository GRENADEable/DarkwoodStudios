using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeKey : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameVariables.Axe += 1;
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
