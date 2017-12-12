using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;

    public GameManager GetInstance()
    {
        return gm;
    }

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(this);
        }

        gm = this;
    }
    // Use this for initialization
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}