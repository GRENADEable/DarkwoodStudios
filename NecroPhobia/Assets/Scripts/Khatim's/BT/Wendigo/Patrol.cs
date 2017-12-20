using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{

    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        //Storing the V3 position of the waypoints.
        Vector3 target = ownerBT.path.Positions[ownerBT.currPosIndex];
        target.y = ownerBT.transform.position.y;
        ownerBT.distanceToWaypoint = Vector3.Distance(target, ownerBT.transform.position);
        ownerBT.transform.LookAt(target);
        ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, target, ownerBT.enemyWalkingSpeed * Time.deltaTime);
        if (ownerBT.distanceToWaypoint < 1.8f)
        {
            ownerBT.currPosIndex++;
        }
        if (ownerBT.currPosIndex == ownerBT.path.Positions.Length)
        {
            ownerBT.currPosIndex = 0;
        }
    }
}
