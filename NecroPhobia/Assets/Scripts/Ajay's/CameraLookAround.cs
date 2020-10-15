using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("Mouse Settings")]
    public float mouseSens = 100f;
    public Transform playerBod;

    private float m_xRotate = 0f;
    private float m_timer = 0.0f;
    private float m_bobSpeed = 0.2f;
    private float m_bobamount = 0.1f;
    [SerializeField] private float m_midpoint = 0.85f;

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        m_xRotate -= mouseY;
        m_xRotate = Mathf.Clamp(m_xRotate, -90f, 90f);

        if (gameManagerData.player == GameManagerData.PlayerState.Moving)
        {
            transform.localRotation = Quaternion.Euler(m_xRotate, 0f, 0f);
            playerBod.Rotate(Vector3.up * mouseX);
        }
    }

    void FixedUpdate()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            m_timer = 0.0f;
        else
        {
            //Using a sine wave to determine where the head should be in its bounce
            waveslice = Mathf.Sin(m_timer);
            m_timer = m_timer + m_bobSpeed;
            if (m_timer > Mathf.PI * 2)
                m_timer = m_timer - (Mathf.PI * 2);
        }
        //If the point on the sine wave is not at zero
        if (waveslice != 0)
        {
            // If waveslice is -1, reached the lowest point on the sine wave and should  footstep.
            float translateChange = waveslice * m_bobamount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = m_midpoint + translateChange;
        }
        else
            cSharpConversion.y = m_midpoint;

        if (gameManagerData.player == GameManagerData.PlayerState.Moving)
            transform.localPosition = cSharpConversion;
    }
}
