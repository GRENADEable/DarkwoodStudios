using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{
    private int currPosIndex;

    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        ownerBT.distanceToWaypoint = Vector3.Distance(ownerBT.path.Positions[currPosIndex], ownerBT.transform.position);
        ownerBT.transform.LookAt(ownerBT.path.Positions[currPosIndex]);
        ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, ownerBT.path.Positions[currPosIndex], 
        ownerBT.enemyWalkingSpeed * Time.deltaTime);
        if (ownerBT.distanceToWaypoint < 1.8f)
        {
            currPosIndex++;
        }
        if (currPosIndex == ownerBT.path.Positions.Length)
        {
            currPosIndex = 0;
        }
    }
}
