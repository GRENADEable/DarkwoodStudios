using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WendigoAI : MonoBehaviour
{
    [Space, Header("Enums")]
    public WendigoState aiState = WendigoState.Idle;
    public enum WendigoState { Idle, Wander, Chase, Attack };

    [Space, Header("Wander Variables")]
    public float wanderRadius;
    public float wanderTimer;

    private NavMeshAgent _wendigoAgent;
    private float _timer;

    void Start()
    {
        _wendigoAgent = GetComponent<NavMeshAgent>();
        _timer = wanderTimer;

    }

    void Update()
    {
        switch (aiState)
        {
            case WendigoState.Idle:
                break;

            case WendigoState.Wander:
                Wander();
                break;

            case WendigoState.Chase:
                break;

            case WendigoState.Attack:
                break;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
    }

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

        NavMeshHit navHit;

        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);

        return navHit.position;
    }
}