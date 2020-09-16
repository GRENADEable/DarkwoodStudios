using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerV2 : MonoBehaviour
{
    [Space, Header("Player References")]
    public float playerSpeed;
    public float playerRunSpeed;
    public float gravity = -9.81f;
    private CharacterController _charControl;
    private Vector3 _vel;

    [Space, Header("Cam Refernces")]
    public float fovVal;
    [Range(0f, 0.1f)] public float lerpTime;
    private Camera _cam;
    private float _currFov;

    [Space, Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool _isGrounded;

    //[Space, Header("HUD")]
    //public Slider staminaSlider;
    //public GameObject staminaBar;
    //public GameObject fadeScreen;
    //public GameObject pickupText;
    //public GameObject hatchetIcon;
    //public GameObject invisIcon;


    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _cam = Camera.main;
        _currFov = _cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButton(1))
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, fovVal, lerpTime);
        else
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _currFov, lerpTime);

        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded & _vel.y < 0)
            _vel.y = -2f;

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove + transform.forward * zMove).normalized;

        _charControl.Move(moveDirection * playerSpeed * Time.deltaTime);
        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }
}