using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public enum Condition { Ready, Success, Running, Fail };
    public Condition currCondition = Condition.Ready;

    public List<Node> children = new List<Node>();

    public virtual void Execute(EnemyBehaviourTree ownerBT)
    {
        Debug.Log("The State is Ready");
    }
}
