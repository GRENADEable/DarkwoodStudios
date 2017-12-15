using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public GameObject enemyPrefab;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyPrefab.SetActive(false);
        }
    }
}
