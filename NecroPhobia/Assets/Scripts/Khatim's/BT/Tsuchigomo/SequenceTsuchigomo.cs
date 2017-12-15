using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SequenceTsuchigomo : NodeTsuchigomo
{
    public override void Run(EnemyBTTsuchigomo ownerBT)
    {
        for (int i = 0; i < children.Count; i++)
        {
            children[i].Run(ownerBT);

            //Node will fail
            if (children[i].currCondition == Condition.Fail)
            {
                currCondition = Condition.Fail;
            }

            //Node Running
            if (children[i].currCondition == Condition.Running)
            {
                currCondition = Condition.Running;
            }
        }

        currCondition = Condition.Success;
        return;
    }
}
