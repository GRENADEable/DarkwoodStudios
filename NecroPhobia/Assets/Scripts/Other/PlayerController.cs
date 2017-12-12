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

    [Header("Score")]
    private int score = 0;
    public Text textScore;

    [Header("Invisibility Timer")]
    public float invisTimer;

    [Header("GameObjects")]
    public GameObject relicPickupText;
    public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject spiderEnemy;
    public GameObject relicWhole;

    [Header("Audio")]
    private AudioSource aud;
    public AudioClip relicPickup;

    [Header("Documents")]
    public GameObject lostDoc1;
    public GameObject lostDoc2;
    public GameObject lostDoc3;
    public GameObject lostDoc4;

    void Start()
    {
        relicWhole.SetActive(false);
        spiderEnemy.SetActive(false);
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
        aud = GetComponent<AudioSource>();
        relicPickupText.SetActive(false);

        lostDoc1.SetActive(false);
        /*lostDoc2.SetActive(false);
        lostDoc3.SetActive(false);
        lostDoc4.SetActive(false);*/

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

        if (invisTimer <= 5)
        {
            invisTimer = invisTimer - 1 * Time.deltaTime;
            invisTimer = Mathf.Clamp(invisTimer, 0, 5);
        }
    }
    void OnTriggerStay(Collider relic)
    {
        if (relic.tag == "Document")
        {
            relicPickupText.SetActive(true);

            if (relic.tag == "Document" && Input.GetKeyDown(KeyCode.E))
            {
                lostDoc1.SetActive(!lostDoc1.activeSelf);
            }
        }

        /*if (relic.tag == "Document2")
        {
            relicPickupText.SetActive(true);

            if (relic.tag == "Document2" && Input.GetKeyDown(KeyCode.E))
            {
                lostDoc1.SetActive(!lostDoc1.activeSelf);
            }
        }

        if (relic.tag == "Document3")
        {
            relicPickupText.SetActive(true);

            if (relic.tag == "Document3" && Input.GetKeyDown(KeyCode.E))
            {
                lostDoc1.SetActive(!lostDoc1.activeSelf);
            }
        }

        if (relic.tag == "Document4")
        {
            relicPickupText.SetActive(true);

            if (relic.tag == "Document" && Input.GetKeyDown(KeyCode.E))
            {
                lostDoc1.SetActive(!lostDoc1.activeSelf);
            }
        }*/

        if (relic.tag == "Relic")
        {
            relicPickupText.SetActive(true);
            if (relic.gameObject.tag == "Relic" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);

                score++;
                textScore.text = score.ToString();
                relicPickupText.SetActive(false);
                aud.PlayOneShot(relicPickup, 0.5f);
            }
        }

        if (relic.tag == "Hatchet")
        {
            relicPickupText.SetActive(true);

            if (relic.tag == "Hatchet" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);
                relicPickupText.SetActive(false);
                GameVariables.Axe += 1;
                aud.PlayOneShot(relicPickup, 0.5f);
            }
        }

        if (relic.tag == "RelicEnded" && Input.GetKey(KeyCode.E) && score == 6)
        {
            relicWhole.SetActive(true);
            Debug.Log("Game Ended");
            spiderEnemy.SetActive(false);
        }

        if (relic.tag == "TalisMan" && Input.GetKey(KeyCode.E))
        {
            invisTimer = 5.0f;
            Destroy(relic.gameObject);
        }

       
    }

    void OnTriggerExit(Collider exit)
    {
        if (exit.tag == "Relic")
        {
            relicPickupText.SetActive(false);
        }

        if (exit.tag == "Document")
        {
            relicPickupText.SetActive(false);
            lostDoc1.SetActive(false);
        }

        /*if (exit.tag == "Document2")
        {
            relicPickupText.SetActive(false);
            lostDoc2.SetActive(false);
        }

        if (exit.tag == "Document3")
        {
            relicPickupText.SetActive(false);
            lostDoc3.SetActive(false);
        }

        if (exit.tag == "Document4")
        {
            relicPickupText.SetActive(false);
            lostDoc4.SetActive(false);
        }*/
    }
    void EffectedStamina()
    {
        currStamina = 0;
        StaminaSlider.value = 0;
        Destroy(StaminaBar);
        walkingSpeed = 11;
    }
}
