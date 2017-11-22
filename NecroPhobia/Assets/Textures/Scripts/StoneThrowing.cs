using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrowing : MonoBehaviour
{

    public GameObject stonePrefab;
    GameObject stonePrefabClone;

    private bool isThrowing;

	void Start ()
    {
                    
	}
	
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space))
        {
            isThrowing = true;
        }
        else
            isThrowing = false;

        if (isThrowing == true)
        {
            stonePrefabClone = Instantiate(stonePrefab, transform.position, Quaternion.identity) as GameObject;
            //To throw the stone in the direction the player is facing
            stonePrefabClone.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody rb = stonePrefabClone.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 30;
        }
    }
}
