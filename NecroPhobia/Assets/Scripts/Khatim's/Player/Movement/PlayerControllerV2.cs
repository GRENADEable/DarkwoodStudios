using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControllerV2 : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    //[Space, Header("Player References")]
    [SerializeField] private float _currPlayerSpeed;
    private Vector3 _vel;
    private CharacterController _charControl;
    private PlayerInteraction _plyInteract;

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

    public delegate void SendEvents();
    public static event SendEvents onRelicTriggerEnter;
    public static event SendEvents onRelicTriggerExit;


    void Start()
    {
        _charControl = GetComponent<CharacterController>();
        _cam = Camera.main;
        _currFov = _cam.fieldOfView;
    }

    void Update()
    {
        if (Input.GetMouseButton(1) && gameManagerData.player == GameManagerData.PlayerState.Moving)
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, fovVal, lerpTime);
        else
            _cam.fieldOfView = Mathf.Lerp(_cam.fieldOfView, _currFov, lerpTime);

        if (Input.GetButtonDown("Interact") && _plyInteract != null && gameManagerData.player == GameManagerData.PlayerState.Moving)
            _plyInteract.StartInteraction();
        else if (Input.GetButton("Interact") && _plyInteract != null && gameManagerData.player == GameManagerData.PlayerState.Moving)
            _plyInteract.UpdateInteraction();

        if (Input.GetButtonUp("Interact") && _plyInteract != null && gameManagerData.player == GameManagerData.PlayerState.Moving)
            _plyInteract.EndInteraction();


        if (gameManagerData.currStamina > 0 && Input.GetButton("Run"))
            _currPlayerSpeed = gameManagerData.playerRunSpeed;
        else
            _currPlayerSpeed = gameManagerData.playerWalkSpeed;

        _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (_isGrounded & _vel.y < 0)
            _vel.y = -2f;

        float xMove = Input.GetAxis("Horizontal");
        float zMove = Input.GetAxis("Vertical");

        Vector3 moveDirection = (transform.right * xMove + transform.forward * zMove).normalized;

        if (gameManagerData.player == GameManagerData.PlayerState.Moving)
            _charControl.Move(moveDirection * _currPlayerSpeed * Time.deltaTime); // For directional movement

        _vel.y += gameManagerData.gravity * Time.deltaTime;
        _charControl.Move(_vel * Time.deltaTime); // For gravity
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Relic") && _plyInteract == null)
        {
            _plyInteract = GetComponent<ObjectPickup>();
            _plyInteract.interactCol = other;

            if (Input.GetButton("Interact"))
                _plyInteract.StartInteraction();

            if (onRelicTriggerEnter != null) // Event Sent to UIManager Script
                onRelicTriggerEnter();
            //Debug.Log("Object Reference Added");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (_plyInteract != null)
        {
            if (other.CompareTag("Relic") && _plyInteract.interactCol == other)
            {
                ResetInteraction();
                if (onRelicTriggerExit != null) // Event Sent to UIManager Script
                    onRelicTriggerExit();
                //Debug.Log("Object Reference Removed");
            }
        }
    }

    void ResetInteraction()
    {
        _plyInteract.interactCol = null;
        gameManagerData.player = GameManagerData.PlayerState.Moving;
        _plyInteract = null;
    }
}