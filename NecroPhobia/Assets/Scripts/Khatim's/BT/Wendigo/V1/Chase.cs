using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chase : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        //Tranforms the direction from local space to world space.
        Vector3 chase = ownerBT.transform.TransformDirection(Vector3.forward) * ownerBT.chaseDistance;
        Debug.DrawRay(ownerBT.transform.position, chase, Color.green);

        ownerBT.tarDir = ownerBT.player.transform.position - ownerBT.transform.position;
        ownerBT.angle = Vector3.Angle(ownerBT.tarDir, ownerBT.transform.forward);
        ownerBT.distanceToPlayer = Vector3.Distance(ownerBT.player.position, ownerBT.transform.position);

        if (((ownerBT.angle < 60 && ownerBT.distanceToPlayer < ownerBT.chaseDistance) || ownerBT.distanceToPlayer < ownerBT.closeDistance)
            && ownerBT.player.GetComponent<PlayerController>().invisTimer <= 0)
        {
            currCondition = Condition.Running;
            //The LookAt function. The difference between the position of Y is causing a change in rotation of X.
            ownerBT.transform.LookAt(ownerBT.player.position);
            ownerBT.transform.rotation = Quaternion.Euler(0, ownerBT.transform.rotation.eulerAngles.y, ownerBT.transform.eulerAngles.z);
            ownerBT.anim.SetInteger("Transition", 8);
            ownerBT.transform.position = Vector3.MoveTowards(ownerBT.transform.position, ownerBT.player.transform.position,
            ownerBT.enemyRunningSpeed * Time.deltaTime);
            //Debug.Log("Player Sighted");
        }
        else
        {
            ownerBT.anim.SetInteger("Transition", 3);
            currCondition = Condition.Fail;
            //Debug.Log("Player Out of Sight");
        }

        if (ownerBT.distanceToPlayer < ownerBT.attackDistance && ownerBT.player.GetComponent<PlayerController>().invisTimer <= 0)
        {
            currCondition = Condition.Success;
            //Debug.Log("Player Reached");
        }
    }
}
