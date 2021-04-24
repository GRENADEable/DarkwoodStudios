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
    public float playerWalkSpeed = 5f;
    public float playerRunSpeed = 9f;
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

    [Space, Header("Interaction")]
    public LayerMask interactionLayer;
    public float rayDistance = 3f;

    public delegate void SendEvents();
    public static event SendEvents OnTreeChop;
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
    private bool _staminaCheat;

    [Header("Ground Check")]
    private bool _isGrounded;

    [Header("Interaction")]
    private bool _isAxeCollected;
    private bool _isInteracting;
    private RaycastHit _hit;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        ExamineSystem.OnHatchetDestroy += OnHatchetDestroyEventReceived;
    }

    void OnDisable()
    {
        ExamineSystem.OnHatchetDestroy += OnHatchetDestroyEventReceived;
    }

    void OnDestroy()
    {
        ExamineSystem.OnHatchetDestroy += OnHatchetDestroyEventReceived;
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
        RaycastInteractCheck();
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

    #region Checks
    void GroundCheck()
    {
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded & _vel.y < 0)
            _vel.y = -2f;

        _vel.y += gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime);
    }

    void RaycastInteractCheck()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        _isInteracting = Physics.Raycast(ray, out _hit, rayDistance, interactionLayer);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, _isInteracting ? Color.red : Color.white);

        if (_isInteracting && _isAxeCollected)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                _isAxeCollected = false;
                OnTreeChop?.Invoke();
            }
        }
    }
    #endregion

    #region Movement
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
        if (!_staminaCheat)
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
        else
        {
            staminaSlider.value = maxStamina;
            _canRun = true;

            if (Input.GetButton("Run"))
                _currSpeed = playerRunSpeed;
            else
                _currSpeed = playerWalkSpeed;
        }
    }
    #endregion

    void Zoom()
    {
        if (Input.GetMouseButton(1) && gameManagerData.currPlayerState == GameManagerData.PlayerState.Moving)
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, fovVal, lerpTime);
        else
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _currFov, lerpTime);
    }

    #region Cheats
    public void OnClick_SuperSpeed()
    {
        playerRunSpeed = 50f;
    }

    public void OnClick_CollectAxe()
    {
        _isAxeCollected = true;
    }

    public void OnClick_UnlimitedStamina()
    {
        _staminaCheat = !_staminaCheat;
    }
    #endregion

    #endregion

    #region Events
    void OnHatchetDestroyEventReceived()
    {
        _isAxeCollected = true;
    }
    #endregion
}