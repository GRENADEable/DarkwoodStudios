using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadBobbing : MonoBehaviour {

    private float Timer = 0.0f;
    float BobSpeed = 0.18f;
    float BobAmount = 0.18f;
    float MidPoint = 1f;

    void FixedUpdate()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
        {
            Timer = 0.0f;
        }
        else
        {
            //Using a sine wave to determine where the head should be in its bounce
            waveslice = Mathf.Sin(Timer);
            Timer = Timer + BobSpeed;
            if (Timer > Mathf.PI * 2)
            {
                Timer = Timer - (Mathf.PI * 2);
            }
        }
        //If the point on the sine wave is not at zero
        if (waveslice != 0)
        {
            // If waveslice is -1, reached the lowest point on the sine wave and should  footstep.
            float translateChange = waveslice * BobAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = MidPoint + translateChange;
        }
        else
        {
            cSharpConversion.y = MidPoint;
        }

        transform.localPosition = cSharpConversion;
    }

    void Start ()
    {
		
	}
	
}
