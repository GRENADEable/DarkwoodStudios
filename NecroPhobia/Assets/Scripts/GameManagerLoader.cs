using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerLoader : MonoBehaviour
{
    public GameObject gameManage;

    void Awake()
    {
        if (GameManager.gm == null)
            Instantiate(gameManage);

    }
}
