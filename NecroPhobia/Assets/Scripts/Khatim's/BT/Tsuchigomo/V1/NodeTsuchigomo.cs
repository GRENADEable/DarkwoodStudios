using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeTsuchigomo
{
    public enum Condition { Ready, Success, Running, Fail };
    public Condition currCondition = Condition.Ready;

    public List<NodeTsuchigomo> children = new List<NodeTsuchigomo>();

    public virtual void Run(EnemyBTTsuchigomo ownerBT)
    {
        Debug.Log("State is Ready");
    }
}
