using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerStats : MonoBehaviour {

    public Slider StaminaSlider;
    public float MaxStamina;

    public float currStamina;
    public float regenStamina;

    private CharacterController charControl;

    void Start()
    {
        StaminaSlider.maxValue = MaxStamina;
        StaminaSlider.value = MaxStamina;
        currStamina = MaxStamina;
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            StaminaSlider.value -= Time.deltaTime;
            currStamina -= Time.deltaTime;
        }
        else
        {
            StaminaSlider.value += regenStamina;
            currStamina += regenStamina;
        }

        currStamina = Mathf.Clamp(currStamina, 0, MaxStamina);
        if (StaminaSlider.value >= MaxStamina)
        {
            StaminaSlider.value = MaxStamina;
        }
        
    }
}
