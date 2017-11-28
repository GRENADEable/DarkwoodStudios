using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokeTreeGate : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && GameVariables.Axe > 0)
        {
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
