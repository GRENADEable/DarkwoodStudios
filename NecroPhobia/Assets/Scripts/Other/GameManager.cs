using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;

    [Header("GameObjects")]
    public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject spiderEnemy;

    [Header("Score")]
    [HideInInspector] public int score = 0;
    public Text textScore;


    public static GameManager GetInstance()
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

    void Update()
    {
        if (score == 5)
        {
            Destroy(GameObject.FindGameObjectWithTag("RockDoor"));
        }
    }
}