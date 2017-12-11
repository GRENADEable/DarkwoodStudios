using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        if (ownerBT.angle < 60 && ownerBT.distanceToPlayer < ownerBT.attackDistance 
            && ownerBT.player.GetComponent<PlayerController>().invisTimer <= 0)//Player is Alive
        {
            ownerBT.anim.SetInteger("Transition", 7);
            currCondition = Condition.Success;
            //Debug.Log("Attacking");
        }
        else
        {
            ownerBT.anim.SetInteger("Transition", 3);
            currCondition = Condition.Fail;
            return;
        }
    }
}
