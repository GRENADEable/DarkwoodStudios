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
    public float distanceToPlayer;
    public float angle;
    [HideInInspector] public Vector3 tarDir;
    public float distanceToWaypoint;

    public Animator anim;
    // Use this for initialization
    void Start ()
    {
        anim = GetComponentInChildren<Animator>();
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
}
