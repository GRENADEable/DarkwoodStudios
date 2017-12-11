using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBTTsuchigomo : MonoBehaviour
{
    [HideInInspector] public NodeTsuchigomo root;
    public float enemySpeed;

    public GameObject player;
    public float attackDistance;
    public float distanceToPlayer;
    public float angle;

    [HideInInspector] public Vector3 tarDir;
    [HideInInspector] public Animator anim;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponentInChildren<Animator>();

        SequenceTsuchigomo sequenceNode = new SequenceTsuchigomo();

        root = sequenceNode;
        sequenceNode.children.Add(new ChaseTsuchigomo());
        sequenceNode.children.Add(new AttackTsuchigomo());
        //rg = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update ()
    {
        root.Run(this);
	}
}
