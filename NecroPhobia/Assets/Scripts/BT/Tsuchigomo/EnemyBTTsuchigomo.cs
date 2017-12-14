﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBTTsuchigomo : MonoBehaviour
{
    [HideInInspector] public NodeTsuchigomo root;
    public float enemySpeed;

    public GameObject player;
    public float attackDistance;
    public float chaseDistance;
    public float distanceToPlayer;
    public float angle;
    public float counter;
    public GameObject changeAnim;

    [HideInInspector] public Vector3 tarDir;
    [HideInInspector] public Animator anim;
    [HideInInspector] public CapsuleCollider capcol;

    void Awake()
    {
        capcol = GetComponent<CapsuleCollider>();
    }
    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();

        SequenceTsuchigomo sequenceNode = new SequenceTsuchigomo();

        root = sequenceNode;
        sequenceNode.children.Add(new ChaseTsuchigomo());
        sequenceNode.children.Add(new AttackTsuchigomo());
        counter = 4.2f;
    }

    // Update is called once per frame
    void Update ()
    {
        counter = counter - 1 * Time.deltaTime;
        root.Run(this);
    }
}
