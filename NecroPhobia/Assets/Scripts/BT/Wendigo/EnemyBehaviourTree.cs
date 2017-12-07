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
    public bool isPlaying;
    [HideInInspector] public Animator anim;
    [HideInInspector] public Collider col;
    [HideInInspector] public AudioSource aud;
    public AudioClip audclip;
    public AudioClip chaseClip;
    public AudioClip environmentClip;

    // Use this for initialization
    void Start ()
    {
        isPlaying = false;
        anim = GetComponentInChildren<Animator>();
        col = GetComponentInChildren<Collider>();
        aud = GetComponent<AudioSource>();

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
