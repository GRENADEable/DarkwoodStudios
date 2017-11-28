using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindClosest : MonoBehaviour {

    public GameObject[] waypoints;
    //Animator anim;
    float rotSpeed = 1f;
    float Speed = 11f;
    float AccWP = 10f;
    int CurrentWP = 0;

    List<Transform> path = new List<Transform>();

	void Start ()
    {
        //anim = GetComponent<Animator>();
        foreach (GameObject go in waypoints)
        {
            path.Add(go.transform);
        }
        CurrentWP = FindClosestWP();
        //animation.setBool("isWalking", true);
	}
	
	int FindClosestWP()
    {
        if (path.Count == 0)
        {
            return -1;
        }
        //Have to start somewhere
        int Closest = 0;
        //To store the distance to Closest 
        float lastDist = Vector3.Distance(this.transform.position, path[0].position);
        for (int i =1; i < path.Count; i++)
        {
            //Calculate the distance between the Character and the first waypoint
            float thisDist = Vector3.Distance(this.transform.position, path[i].position);
            //If the current waypoint is closer to the character than the previous waypoint;
            if (lastDist > thisDist && i !=CurrentWP)
            {
                Closest = i;
            }
        }
        return Closest;
    }
	void Update ()
    {
        Vector3 direction = path[CurrentWP].position - transform.position;
        this.transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotSpeed * Time.deltaTime);
        this.transform.Translate(0, 0, Time.deltaTime * Speed);
        //Once the character reaches the waypoint, move on to the next waypoint;
        if (direction.magnitude < AccWP)
        {
            path.Remove(path[CurrentWP]);
            CurrentWP = FindClosestWP();
        }
	}
}
