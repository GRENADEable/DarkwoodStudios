using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterController : MonoBehaviour
{
    public Slider StaminaSlider;
    public GameObject StaminaBar;
    public float MaxStamina;

    public float currStamina;
    public float regenStamina;

    public float walkingSpeed;
    public float runningSpeed;


    private int score = 0;
    public Text textScore;

    public GameObject openGateDoor;
    public GameObject closeGateDoor;
    public GameObject relicWhole;
    public GameObject spiderEnemy;

    public float spawnDistance;

    void Start()
    {
        relicWhole.SetActive(false);
        spiderEnemy.SetActive(false);
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
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
    }

    void OnTriggerStay(Collider relic)
    {
        if (relic.gameObject.tag == "Relic" && Input.GetKey(KeyCode.E))
        {
            Destroy(relic.gameObject);

            score++;
            textScore.text = score.ToString();
        }

        if (relic.gameObject.tag == "RelicEnded" && Input.GetKey(KeyCode.E) && score == 6)
        {
            relicWhole.SetActive(true);
            Debug.Log("Game Ended");
            Destroy(spiderEnemy);
        }
    }

    void EffectedStamina()
    {
        currStamina = 0;
        StaminaSlider.value = 0;
        Destroy(StaminaBar);
    }
}
