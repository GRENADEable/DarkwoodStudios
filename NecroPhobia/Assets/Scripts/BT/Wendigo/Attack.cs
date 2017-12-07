using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Attack : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        if (ownerBT.angle < 40 && ownerBT.distanceToPlayer < ownerBT.attackDistance)//Player is Alive
        {
            ownerBT.player = null;
            //ownerBT.anim.SetInteger("Transition", 7);
            currCondition = Condition.Success;
            Debug.Log("Attacking");
        }
        else
        {
            //ownerBT.anim.SetInteger("Transition", 3);

            currCondition = Condition.Fail;
            return;
        }

        if (ownerBT.player == null)//Player is Dead
        {
            SceneManager.LoadScene("DeathScene");
            currCondition = Condition.Fail;
            Debug.Log("Player is Dead");
        }
    }
}
