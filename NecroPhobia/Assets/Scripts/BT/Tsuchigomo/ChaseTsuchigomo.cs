using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseTsuchigomo : NodeTsuchigomo
{
    public override void Run(EnemyBTTsuchigomo ownerBT)
    {
        ownerBT.distanceToPlayer = Vector3.Distance(ownerBT.player.transform.position, ownerBT.transform.position);
        
        if (GameObject.FindGameObjectWithTag("Player") && ownerBT.counter <=0)
        {
            currCondition = Condition.Running;
            ownerBT.transform.LookAt(ownerBT.player.transform.position);
            ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, ownerBT.player.transform.position, 
            ownerBT.enemySpeed * Time.deltaTime);
            //Debug.Log("Player Sighted");
        }
        else
        {
            currCondition = Condition.Fail;
            //Debug.Log("Player Out of Sight");
        }

        if (ownerBT.distanceToPlayer < ownerBT.attackDistance)
        {
            currCondition = Condition.Success;
            //Debug.Log("Player Reached");
        }
    }
}
