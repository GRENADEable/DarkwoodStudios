using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Stamina Bars")]
    public Slider StaminaSlider;
    public GameObject StaminaBar;

    [Header("Stamina")]
    public float currStamina;
    public float regenStamina;
    public float MaxStamina;

    [Header("Invisibility")]
    public float invisTimer;

    [Header("Movement Speeds")]
    public float walkingSpeed;
    public float runningSpeed;

    [Header("Icons")]
    public GameObject fadeScreen;
    public GameObject pickupText;
    public GameObject hatchetIcon;
    public GameObject invisIcon;

    [Header("GameObjects")]
    public GameObject relicWhole;
    public GameObject wendigoEnemy;
    public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject spiderEnemy;
    public GameObject laternsPrefab;
    public GameObject[] rockDoors;

    [Header("Audio")]
    public AudioClip relicPickup;
    public AudioClip rockNotify;
    private AudioSource aud;

    [Header("Score")]
    public Text textScore;
    public GameObject Score;
    [HideInInspector] public int score = 0;

    void Start()
    {
        //Cursor.visible = false;
        //Cursor.lockState = CursorLockMode.Locked;

        relicWhole.SetActive(false);
        hatchetIcon.SetActive(false);
        spiderEnemy.SetActive(false);
        fadeScreen.SetActive(false);

        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
        GameVariables.Axe = 0;


        aud = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //Cursor.visible = true;
            //Cursor.lockState = CursorLockMode.None;

            //SceneManager.LoadScene("MainMenu");
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            StaminaSlider.value -= Time.deltaTime;
            currStamina -= Time.deltaTime;
        }
        else
        {
            StaminaSlider.value += regenStamina * Time.deltaTime;
            currStamina += regenStamina * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftShift) && currStamina > 0)
        {
            float translation2 = Input.GetAxis("Vertical") * runningSpeed;
            float straffe2 = Input.GetAxis("Horizontal") * runningSpeed;

            translation2 *= Time.deltaTime;
            straffe2 *= Time.deltaTime;

            transform.Translate(straffe2, 0f, translation2);
        }

        else
        {
            float translation = Input.GetAxis("Vertical") * walkingSpeed;
            float straffe = Input.GetAxis("Horizontal") * walkingSpeed;

            translation *= Time.deltaTime;
            straffe *= Time.deltaTime;

            transform.Translate(straffe, 0f, translation);
        }
        currStamina = Mathf.Clamp(currStamina, 0, MaxStamina);

        if (StaminaSlider.value >= MaxStamina)
        {
            StaminaSlider.value = MaxStamina;
        }

        if (invisTimer <= 5)
        {
            invisTimer -= Time.deltaTime;
            invisTimer = Mathf.Clamp(invisTimer, 0, 5);
        }

        if (invisTimer <= 0)
        {
            invisIcon.SetActive(false);
        }

        if (score == 6)
        {
            EffectedStamina();
            closeGateDoor.SetActive(false);
            openGateDoor.SetActive(true);
            laternsPrefab.SetActive(true);

        }
        else
        {
            laternsPrefab.SetActive(false);
            closeGateDoor.SetActive(true);
            openGateDoor.SetActive(false);
        }
    }
    void OnTriggerStay(Collider relic)
    {
        if (relic.tag == "Relic")
        {
            pickupText.SetActive(true);
            if (relic.gameObject.tag == "Relic" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);

                score++;
                textScore.text = score.ToString();
                pickupText.SetActive(false);
                aud.PlayOneShot(relicPickup, 0.8f);
            }
        }
        if (relic.tag == "Relic" && score == 5)
        {
            rockDoors[0].SetActive(false);
            rockDoors[1].SetActive(false);
            rockDoors[2].SetActive(false);
            aud.PlayOneShot(rockNotify, 0.8f);
        }

        if (relic.tag == "Hatchet")
        {
            pickupText.SetActive(true);

            if (relic.tag == "Hatchet" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);
                pickupText.SetActive(false);
                hatchetIcon.SetActive(true);
                GameVariables.Axe += 1;
                aud.PlayOneShot(relicPickup, 0.7f);
            }
        }

        if (relic.tag == "RelicEnded" && Input.GetKey(KeyCode.E) && score == 6)
        {
            relicWhole.SetActive(true);
            Score.SetActive(false);
            fadeScreen.SetActive(true);

            Debug.Log("Game Ended");
        }

        if (relic.tag == "LastPiece")
        {
            pickupText.SetActive(true);
            if (relic.gameObject.tag == "LastPiece" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);

                score++;
                textScore.text = score.ToString();
                pickupText.SetActive(false);
                aud.PlayOneShot(relicPickup, 0.8f);
                Destroy(wendigoEnemy);
                spiderEnemy.SetActive(true);
                rockDoors[1].SetActive(true);
                rockDoors[2].SetActive(true);

            }
        }
        if (relic.tag == "TalisMan")
        {
            pickupText.SetActive(true);
            if (relic.tag == "TalisMan" && Input.GetKey(KeyCode.E))
            {
                invisIcon.SetActive(true);
                pickupText.SetActive(false);
                invisTimer = 5.0f;
                Destroy(relic.gameObject);
            }

        }

    }

    void OnTriggerExit(Collider relic)
    {
        if (relic.tag == "Relic")
        {
            pickupText.SetActive(false);
        }

        if (relic.tag == "Hatchet")
        {
            pickupText.SetActive(false);
        }

        if (relic.tag == "TalisMan")
        {
            pickupText.SetActive(false);
        }
    }

    void EffectedStamina()
    {
        currStamina = 0;
        StaminaSlider.value = 0;
        Destroy(StaminaBar);
        walkingSpeed = 11;
    }
}
