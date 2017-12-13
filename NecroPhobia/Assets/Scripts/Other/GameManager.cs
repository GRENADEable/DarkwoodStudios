using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager gm;
    public PlayerController player;

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
        player = GetComponent<PlayerController>();
        DontDestroyOnLoad(this.gameObject);
        spiderEnemy.SetActive(false);
    }

    void Update()
    {
        if (score == 5)
        {
            Destroy(GameObject.FindGameObjectWithTag("RockDoor"));
        }

        if (score == 6)
        {
            closeGateDoor.SetActive(false);
            openGateDoor.SetActive(true);
            spiderEnemy.SetActive(true);
            EffectedStamina();
        }
        else
        {
            closeGateDoor.SetActive(true);
            openGateDoor.SetActive(false);
        }
    }

    void EffectedStamina()
    {
        player.currStamina = 0;
        player.StaminaSlider.value = 0;
        Destroy(player.StaminaBar);
        player.walkingSpeed = 11;
    }
}