using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLookAround : MonoBehaviour
{
    #region Public Variables
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("Mouse Settings")]
    public float mouseSens = 100f;
    public Transform playerRoot;
    #endregion

    #region Private Variables
    private float _xRotate = 0f;
    private float _timer = 0.0f;
    private float _bobSpeed = 0.2f;
    private float _bobamount = 0.1f;
    [SerializeField] private float _midpoint = 0.85f;
    #endregion

    #region Unity Callbacks
    void Update()
    {
        CamLookAround();
    }

    void FixedUpdate()
    {
        HeadBobbing();
    }
    #endregion

    #region My Functions

    #region Cam Movement
    void CamLookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSens * Time.deltaTime;

        _xRotate -= mouseY;
        _xRotate = Mathf.Clamp(_xRotate, -90f, 90f);

        if (gameManagerData.currPlayerState == GameManagerData.PlayerState.Moving)
        {
            transform.localRotation = Quaternion.Euler(_xRotate, 0f, 0f);
            playerRoot.Rotate(Vector3.up * mouseX);
        }
    }
    #endregion

    #region HeadBob
    void HeadBobbing()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 cSharpConversion = transform.localPosition;

        if (Mathf.Abs(horizontal) == 0 && Mathf.Abs(vertical) == 0)
            _timer = 0.0f;
        else
        {
            //Using a sine wave to determine where the head should be in its bounce
            waveslice = Mathf.Sin(_timer);
            _timer = _timer + _bobSpeed;
            if (_timer > Mathf.PI * 2)
                _timer = _timer - (Mathf.PI * 2);
        }
        //If the point on the sine wave is not at zero
        if (waveslice != 0)
        {
            // If waveslice is -1, reached the lowest point on the sine wave and should  footstep.
            float translateChange = waveslice * _bobamount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            cSharpConversion.y = _midpoint + translateChange;
        }
        else
            cSharpConversion.y = _midpoint;

        if (gameManagerData.currPlayerState == GameManagerData.PlayerState.Moving)
            transform.localPosition = cSharpConversion;
    }
    #endregion

    #endregion
}