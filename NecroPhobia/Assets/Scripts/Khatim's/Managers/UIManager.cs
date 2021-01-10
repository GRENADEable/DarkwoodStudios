using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Space, Header("Data")]
    public GameManagerData gameManagerData;

    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;

    [Space, Header("UI References")]
    public GameObject examineCanvas;
    public Slider staminaSlider;
    public Text relicCountText;

    [Space, Header("Examine Object References")]
    public float examineSpeed;

    [Space, Header("Object Inspection")]
    public Transform objectPickedPos;
    public Vector3 relicScaleVector;
    public Vector3 documentScaleVector;
    private GameObject _examineObj;
    private Vector2 _lastMousePos;

    public delegate void SendEvents();
    public static event SendEvents onExaminExit;

    void OnEnable()
    {
        PlayerControllerV2.onRelicTriggerEnter += OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit += OnRelicTriggerExitEventReceived;

        ObjectPickup.onObjPickup += OnObjPickupEventReceived;
    }

    void OnDisable()
    {
        PlayerControllerV2.onRelicTriggerEnter -= OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit -= OnRelicTriggerExitEventReceived;

        ObjectPickup.onObjPickup -= OnObjPickupEventReceived;
    }

    void OnDestroy()
    {
        PlayerControllerV2.onRelicTriggerEnter -= OnRelicTriggerEnterEventReceived;

        PlayerControllerV2.onRelicTriggerExit -= OnRelicTriggerExitEventReceived;

        ObjectPickup.onObjPickup -= OnObjPickupEventReceived;
    }

    void Start()
    {
        staminaSlider.maxValue = gameManagerData.maxStamina;
        staminaSlider.value = gameManagerData.maxStamina;
        relicCountText.text = "Relic Count: " + gameManagerData.relicCount;
    }

    void Update()
    {
        if (gameManagerData.player == GameManagerData.PlayerState.Examine)
        {
            Vector2 currMousePos = (Vector2)Input.mousePosition;
            Vector2 mouseDelta = currMousePos - _lastMousePos;
            mouseDelta *= examineSpeed * Time.deltaTime;

            _lastMousePos = currMousePos;

            if (Input.GetMouseButton(0))
                _examineObj.transform.Rotate(mouseDelta.y * 1, mouseDelta.x * -1f, 0f, Space.World);

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ExamineExit();
                if (onExaminExit != null)
                    onExaminExit();
            }
        }

        if (Input.GetButton("Run"))
        {
            staminaSlider.value -= gameManagerData.depleteStamina * Time.deltaTime;
            gameManagerData.currStamina -= gameManagerData.depleteStamina * Time.deltaTime;
        }
        else
        {
            staminaSlider.value += gameManagerData.regenStamina * Time.deltaTime;
            gameManagerData.currStamina += gameManagerData.regenStamina * Time.deltaTime;
        }
    }

    void OnRelicTriggerEnterEventReceived()
    {
        relicPickupTxt.SetActive(true);
    }

    void OnRelicTriggerExitEventReceived()
    {
        relicPickupTxt.SetActive(false);
    }

    void OnObjPickupEventReceived(GameObject obj)
    {
        gameManagerData.player = GameManagerData.PlayerState.Examine;

        examineCanvas.SetActive(true);
        relicPickupTxt.SetActive(false);

        GameObject spawnObj = Instantiate(obj, objectPickedPos.position, Quaternion.identity, objectPickedPos);
        Destroy(spawnObj.GetComponent<Collider>());
        Destroy(spawnObj.GetComponent<Rigidbody>());

        if (obj.gameObject.tag == "Relic")
        {
            spawnObj.transform.localScale = relicScaleVector;
            gameManagerData.relicCount++;
            relicCountText.text = "Relic Count: " + gameManagerData.relicCount;
        }
        else if (obj.gameObject.tag == "Document")
            spawnObj.transform.localScale = documentScaleVector;

        _examineObj = spawnObj;

        //Debug.Log("Object Spawn on Screen");
    }

    void ExamineExit()
    {
        gameManagerData.player = GameManagerData.PlayerState.Moving;

        examineCanvas.SetActive(false);
        Destroy(_examineObj);
        _examineObj = null;
    }
}