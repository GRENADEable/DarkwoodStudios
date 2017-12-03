using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        Vector3 chase = ownerBT.transform.TransformDirection(Vector3.forward) * ownerBT.chaseDistance;
        Debug.DrawRay(ownerBT.transform.position, chase, Color.green);

        ownerBT.tarDir = ownerBT.player.transform.position - ownerBT.transform.position;
        ownerBT.angle = Vector3.Angle(ownerBT.tarDir, ownerBT.transform.forward);
        ownerBT.distanceToPlayer = Vector3.Distance(ownerBT.player.position, ownerBT.transform.position);

        if (ownerBT.distanceToPlayer < ownerBT.attackDistance)
        {
            currCondition = Condition.Success;
        }
        if (ownerBT.angle < 20 && ownerBT.distanceToPlayer < ownerBT.chaseDistance)
        {
            currCondition = Condition.Running;
            ownerBT.transform.LookAt(ownerBT.player);
            ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, ownerBT.player.transform.position, ownerBT.enemySpeed * Time.deltaTime);
        }
        else
        {
            currCondition = Condition.Fail;
        }
    }
}
