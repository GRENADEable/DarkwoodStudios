using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequence : Node
{
    public override void Execute(EnemyBehaviourTree ownerBT)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Execute(ownerBT);

            //Node Failed
            if (children[i].currCondition == Condition.Fail)
            {
                currCondition = Condition.Fail;
                return;
            }

            //Node Running
            if (children[i].currCondition == Condition.Running)
            {
                currCondition = Condition.Running;
                return;
            }
        }

        //Node Succeeded
        currCondition = Condition.Success;
        return;
    }
}
