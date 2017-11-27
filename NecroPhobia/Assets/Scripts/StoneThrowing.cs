using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneThrowing : MonoBehaviour
{

    public GameObject stonePrefab;
    

    private bool isThrowing;

	void Start ()
    {
                    
	}
	
	
	void Update ()
    {
		if(Input.GetKeyDown(KeyCode.Space) && GameVariables.Stones > 0)
        {
            isThrowing = true;
        }
        else
            isThrowing = false;

        if (isThrowing == true)
        {
            GameVariables.Stones--;
            GameObject projectile = Instantiate(stonePrefab, transform.position, Quaternion.identity) as GameObject;
            //To throw the stone in the direction the player is facing
            projectile.transform.position = transform.position + Camera.main.transform.forward * 2;
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            rb.velocity = Camera.main.transform.forward * 30;
        }
    }
}
