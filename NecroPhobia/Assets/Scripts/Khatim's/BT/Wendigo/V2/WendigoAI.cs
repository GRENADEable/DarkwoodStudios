using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WendigoAI : MonoBehaviour
{
    #region Public Variables
    public float wanderRadius;
    public float wanderTimer;
    #endregion

    #region Private Variables
    private NavMeshAgent _wendigoAgent;
    private float _timer;
    #endregion

    void Start()
    {
        _wendigoAgent = GetComponent<NavMeshAgent>();
        _timer = wanderTimer;

    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= wanderTimer)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            _wendigoAgent.SetDestination(newPos);
            _timer = 0;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, wanderRadius);
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