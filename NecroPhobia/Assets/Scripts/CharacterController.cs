using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterController : MonoBehaviour
{

    public float walkingSpeed;
    public float runningSpeed;

    private int score = 0;
    public Text textScore;

    void Start()
    {

    }


    void Update()
    {
        float translation = Input.GetAxis("Vertical") * walkingSpeed;
        float straffe = Input.GetAxis("Horizontal") * walkingSpeed;

        translation *= Time.deltaTime;
        straffe *= Time.deltaTime;

        transform.Translate(straffe, 0f, translation);


        if (Input.GetKey(KeyCode.LeftShift))
        {
            float translation2 = Input.GetAxis("Vertical") * runningSpeed;
            float straffe2 = Input.GetAxis("Horizontal") * runningSpeed;

            translation2 *= Time.deltaTime;
            straffe2 *= Time.deltaTime;

            transform.Translate(straffe2, 0f, translation2);
        }

    }

    private void OnCollisionStay(Collision pick)
    {
        if (pick.gameObject.tag == "Relic" && Input.GetKey(KeyCode.E))
        {
            Destroy(pick.gameObject);

            score++;
            textScore.text = score.ToString();
        }
    }
}
