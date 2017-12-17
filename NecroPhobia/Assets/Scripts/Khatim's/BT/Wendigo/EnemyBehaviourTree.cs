using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviourTree : MonoBehaviour
{
    [HideInInspector] public Node root;
    public WayPointSystem path;
    public float enemyWalkingSpeed;
    public float enemyRunningSpeed;
    public Transform player;

    public float chaseDistance;
    public float attackDistance;
    public float closeDistance;
    public float distanceToPlayer;
    public float angle;
    public float distanceToWaypoint;

    [HideInInspector]public int currPosIndex;
    [HideInInspector] public Vector3 tarDir;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider[] capcol;
    [HideInInspector] public Rigidbody rg;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        capcol = GetComponentsInChildren<Collider>();
        rg = GetComponent<Rigidbody>();


        Selector selectNode = new Selector();
        Sequence sequenceNode = new Sequence();

        root = selectNode;
        selectNode.children.Add(sequenceNode);
        selectNode.children.Add(new Patrol());

        sequenceNode.children.Add(new Chase());
        sequenceNode.children.Add(new Attack());
    }
	
	// Update is called once per frame
	void Update ()
    {
        root.Execute(this);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Other")
        {
            rg.useGravity = false;
            capcol[1].enabled = false;
            capcol[2].enabled = false;
        }
        else
        {
            rg.useGravity = true;
            capcol[1].enabled = true;
            capcol[2].enabled = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Collider>().tag == "Other")
        {
            rg.useGravity = true;
            capcol[1].enabled = true;
            capcol[2].enabled = true;
        }
    }
}
