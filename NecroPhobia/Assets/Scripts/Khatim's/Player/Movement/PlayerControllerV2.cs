using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerControllerV2 : MonoBehaviour
{
    #region Public Variables
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("Cam Refernces")]
    public float fovVal;
    [Range(0f, 0.1f)] public float lerpTime;

    [Space, Header("Player Movement")]
    public float playerWalkSpeed = 7f;
    public float playerRunSpeed = 15f;
    public float gravity = -9.81f;

    [Space, Header("Player Stamina")]
    public Slider staminaSlider;
    public float regenStamina = 30f;
    public float depleteStamina = 40f;
    public float maxStamina = 100f;

    [Space, Header("Ground Check")]
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    #endregion

    #region Private Variables
    [Header("Player Movement")]
    private float _currSpeed;
    private Vector3 _vel;
    private Vector3 _moveDirection;
    private CharacterController _charControl;

    [Header("Cam Refernces")]
    private Camera _cam;
    private float _currFov;

    [Header("Player Stamina")]
    private bool _canRun;
    private float _currStamina;

    [Header("Ground Check")]
    private bool _isGrounded;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {

    }

    void OnDisable()
    {

    }

    void OnDestroy()
    {

    }
    #endregion

    #region Intialisation and Loops
    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _cam = Camera.main;
        _currFov = _cam.fieldOfView;
        IntializeStamina();
    }

    void Update()
    {
        GroundCheck();
        PlayerMovement();
        PlayerRunning();
        //Zoom();
    }
    #endregion

    #endregion

    #region My Functions
    void IntializeStamina()
    {
        staminaSlider.maxValue = maxStamina;
        staminaSlider.value = maxStamina;
        _currStamina = maxStamina;
    }

    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded & _vel.y < 0)
            _vel.y = -2f;

        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }

    void PlayerMovement()
    {
        _currStamina = Mathf.Clamp(_currStamina, 0, maxStamina);

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        _moveDirection = (transform.right * xMove + transform.forward * zMove).normalized;

        if (gameManagerData.currPlayerState == GameManagerData.PlayerState.Moving)
            _charControl.Move(_moveDirection * _currSpeed * Time.deltaTime);
    }

    void PlayerRunning()
    {
        if (_currStamina >= 0 && Input.GetButton("Run") && _moveDirection != Vector3.zero && _canRun)
        {
            _currSpeed = playerRunSpeed;
            staminaSlider.value -= depleteStamina * Time.deltaTime;
            _currStamina -= depleteStamina * Time.deltaTime;
        }
        else if (!Input.GetButton("Run"))
        {
            _currSpeed = playerWalkSpeed;
            staminaSlider.value += regenStamina * Time.deltaTime;
            _currStamina += regenStamina * Time.deltaTime;
        }

        if (_currStamina <= 0)
        {
            _canRun = false;
            _currSpeed = playerWalkSpeed;
        }
        else
            _canRun = true;
    }

    void Zoom()
    {
        if (Input.GetMouseButton(1) && gameManagerData.currPlayerState == GameManagerData.PlayerState.Moving)
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, fovVal, lerpTime);
        else
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _currFov, lerpTime);
    }
    #endregion
}