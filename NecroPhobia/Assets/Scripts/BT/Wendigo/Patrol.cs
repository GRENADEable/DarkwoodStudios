using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Node
{
    private int currPosIndex;

    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        Vector3 target = ownerBT.path.Positions[currPosIndex];
        target.y = ownerBT.transform.position.y;
        ownerBT.distanceToWaypoint = Vector3.Distance(target, ownerBT.transform.position);
        ownerBT.transform.LookAt(target);
        ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, target, ownerBT.enemyWalkingSpeed * Time.deltaTime);
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
