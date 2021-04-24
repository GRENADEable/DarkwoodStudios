using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WendigoAI : MonoBehaviour
{
    #region Public Variables
    [Space, Header("Enums")]
    public WendigoState currAIState = WendigoState.Idle;
    public enum WendigoState { Idle, Wander, Chase, Attack };

    [Space, Header("Wander")]
    public float wanderRadius;
    public float wanderTimer;
    public float wanderSpeed = 7f;

    [Space, Header("Chase")]
    public GameObject playerRef;
    public float chaseSpeed = 11f;
    public float chaseRange = 20f;
    #endregion

    #region Private Variables
    [Header("Chase")]
    [SerializeField] private float _distance;
    private NavMeshAgent _wendigoAgent;
    private Animator _wendigoAnim;
    private float _timer;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        currAIState = WendigoState.Wander;
        _wendigoAgent = GetComponent<NavMeshAgent>();
        _wendigoAnim = GetComponent<Animator>();
        _wendigoAgent.speed = wanderSpeed;
        _timer = wanderTimer;
    }

    void Update()
    {
        _distance = Vector3.Distance(transform.position, playerRef.transform.position);
        _wendigoAnim.SetFloat("Speed", _wendigoAgent.velocity.magnitude);
        States();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }
    #endregion

    #region My Functions

    void States()
    {
        switch (currAIState)
        {
            case WendigoState.Idle:
                break;

            case WendigoState.Wander:
                if (_distance <= chaseRange)
                    currAIState = WendigoState.Chase;

                Wander();
                break;

            case WendigoState.Chase:
                if (_distance >= chaseRange)
                    currAIState = WendigoState.Wander;

                Chase();
                break;

            case WendigoState.Attack:
                break;
        }
    }

    #region Wander
    void Wander()
    {
        _timer += Time.deltaTime;

        if (_timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _wendigoAgent.SetDestination(newPos);
            _timer = 0;
        }
    }

    static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMesh.SamplePosition(randDirection, out NavMeshHit navHit, dist, layermask);
        return navHit.position;
    }
    #endregion

    #region Chase
    void Chase()
    {
        _wendigoAgent.speed = chaseSpeed;
        _wendigoAgent.SetDestination(playerRef.transform.position);
    }
    #endregion

    #endregion
}