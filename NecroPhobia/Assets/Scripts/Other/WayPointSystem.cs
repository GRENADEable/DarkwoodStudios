using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPointSystem : MonoBehaviour
{
    public Vector3[] Positions;

    private void Start()
    {
        Positions = new Vector3[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            Positions[i] = transform.GetChild(i).transform.position;
        }
    }
}
