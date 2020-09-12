using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackTsuchigomo : NodeTsuchigomo
{
    public override void Run(EnemyBTTsuchigomo ownerBT)
    {
        if (ownerBT.distanceToPlayer < ownerBT.attackDistance)//Player is Alive
        {
            ownerBT.anim.SetInteger("TsuchiTransition", 2);
            currCondition = Condition.Success;
            //Debug.Log("Attacking");
        }
        else
        {
            ownerBT.anim.SetInteger("TsuchiTransition", 1);
            currCondition = Condition.Fail;
            return;
        }
    }
}
