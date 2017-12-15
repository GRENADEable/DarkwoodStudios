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

    [Header("Movement Speeds")]
    public float walkingSpeed;
    public float runningSpeed;

    [Header("Invisibility Timer")]
    public float invisTimer;

    [Header("GameObjects")]
    public GameObject pickupText;
    public GameObject relicWhole;
    public GameObject wendigoEnemy;
    public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject spiderEnemy;
    public GameObject hatchetIcon;

    [Header("Audio")]
    public AudioClip relicPickup;
    public AudioClip rockNotify;
    private AudioSource aud;

    [Header("Score")]
    public Text textScore;
    [HideInInspector] public int score = 0;

    void Start()
    {
        hatchetIcon.SetActive(false);
        spiderEnemy.SetActive(false);
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
        aud = GetComponent<AudioSource>();

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SceneManager.LoadScene("MainMenu");
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
            invisTimer = invisTimer - 1 * Time.deltaTime;
            invisTimer = Mathf.Clamp(invisTimer, 0, 5);
        }

        if (score == 5)
        {
            Destroy(GameObject.Find("DoorBoulder"));
        }

        if (score == 6)
        {
            EffectedStamina();
            closeGateDoor.SetActive(false);
            openGateDoor.SetActive(true);
        }
        else
        {
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

            }
        }
        if (relic.tag == "TalisMan")
        {
            pickupText.SetActive(true);
            if (relic.tag == "TalisMan" && Input.GetKey(KeyCode.E))
            {
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
