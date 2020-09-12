using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selector : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute(ownerBT);//Run the code and then will check them in the code below.

            //Node Succeeded
            if (children[i].currCondition == Condition.Success)
            {
                currCondition = Condition.Success;
                return;
            }


            //Node Running
            else if (children[i].currCondition == Condition.Running)
            {
                currCondition = Condition.Running;
                return;
            }

        }

        //Node Fail
        currCondition = Condition.Fail;
        return;
    }
}
