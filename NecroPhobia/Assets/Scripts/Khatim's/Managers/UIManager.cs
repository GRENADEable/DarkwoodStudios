using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;

    [Space, Header("Animation Controllers")]
    public Animator effectAnim;

    [Space, Header("Object Inspection")]
    public Transform objectPickedPos;
    public Vector3 scaleVector;
    [SerializeField] private GameObject _pickedObj;

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
    }

    void Update()
    {

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
        _pickedObj = obj;
        GameObject spawnObj = Instantiate(_pickedObj, objectPickedPos.position, Quaternion.identity, objectPickedPos);
        spawnObj.transform.localScale = scaleVector;
        effectAnim.Play("ExamineAppearAnim");
        Debug.Log("Object Spawn on Screen");
    }
}