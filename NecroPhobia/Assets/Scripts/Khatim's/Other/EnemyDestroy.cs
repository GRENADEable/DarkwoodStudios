using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject invisWall;

    void Start()
    {
        invisWall.SetActive(false);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyPrefab.SetActive(false);
            invisWall.SetActive(true);
        }
    }
}
