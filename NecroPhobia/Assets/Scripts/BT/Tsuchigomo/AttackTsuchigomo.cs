using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AttackTsuchigomo : NodeTsuchigomo
{
    public override void Run(EnemyBTTsuchigomo ownerBT)
    {
        if (ownerBT.angle < 40 && ownerBT.distanceToPlayer < ownerBT.attackDistance)//Player is Alive
        {
            ownerBT.player = null;
            currCondition = Condition.Success;
            Debug.Log("Attacking");
        }
        else
        {
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
