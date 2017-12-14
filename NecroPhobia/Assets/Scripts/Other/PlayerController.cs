using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameManager gm;

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
    public GameObject relicPickupText;
    /*public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject spiderEnemy;*/
    public GameObject relicWhole;
    public GameObject wendigoEnemy;

    [Header("Audio")]
    private AudioSource aud;
    public AudioClip relicPickup;

    void Start()
    {
        //relicWhole.SetActive(false);
        gm.spiderEnemy.SetActive(false);
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
        aud = GetComponent<AudioSource>();
        gm = GameManager.GetInstance();//GetComponent<GameManager>();

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

        if (gm.score == 6)
        {
            EffectedStamina();
            gm.closeGateDoor.SetActive(false);
            gm.openGateDoor.SetActive(true);
        }
        else
        {
            gm.closeGateDoor.SetActive(true);
            gm.openGateDoor.SetActive(false);
        }
    }
    void OnTriggerStay(Collider relic)
    {
        if (relic.tag == "Relic")
        {
            relicPickupText.SetActive(true);
            if (relic.gameObject.tag == "Relic" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);

                gm.score++;
                gm.textScore.text = gm.score.ToString();
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

        if (relic.tag == "RelicEnded" && Input.GetKey(KeyCode.E) && gm.score == 6)
        {
            relicWhole.SetActive(true);
            Debug.Log("Game Ended");
        }

        if (relic.tag == "LastPiece")
        {
            relicPickupText.SetActive(true);
            if (relic.gameObject.tag == "LastPiece" && Input.GetKey(KeyCode.E))
            {
                Destroy(relic.gameObject);

                gm.score++;
                gm.textScore.text = gm.score.ToString();
                relicPickupText.SetActive(false);
                aud.PlayOneShot(relicPickup, 0.5f);
                gm.spiderEnemy.SetActive(true);

            }
        }

        if (relic.tag == "TalisMan" && Input.GetKey(KeyCode.E))
        {
            invisTimer = 5.0f;
            Destroy(relic.gameObject);
        }
    }

    void OnTriggerExit(Collider relic)
    {
        if (relic.tag == "Relic")
        {
            relicPickupText.SetActive(false);
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
