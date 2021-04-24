using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExamineSystem : MonoBehaviour
{
    #region Public Variables
    [Space, Header("Data")]
    public GameManagerData gmData;

    [Space, Header("UI References")]
    public GameObject hudPanel;

    [Space, Header("Key Inputs")]
    public KeyCode pickupKey;
    public KeyCode dropKey;

    [Space, Header("Transform References")]
    public Transform examinePointPos;
    public Transform pickPropReturnParentPos;
    public Vector3 scaleDown;

    [Space, Header("Examine Object References")]
    public LayerMask examineLayer;
    public float rayDistance = 0.7f;
    public float rotationSpeedMouse = 40f;

    public delegate void SendEvents();
    public static event SendEvents OnRelicDestroy;
    #endregion

    #region Private Variables
    [Header("Camera")]
    private Camera _cam;

    [Header("UI References")]
    private Transform _rotateObjTransform;

    [Header("Interaction Variables")]
    private RaycastHit _hit;
    private GameObject _tempObjReference;
    private Vector3 _intialObjPos;
    private Quaternion _intialObjRot;
    private Vector3 _intialObjScale;
    private bool _isInteracting;
    private bool _isExamine;
    #endregion

    #region Unity Callbacks
    void Start()
    {
        _cam = Camera.main;
    }

    void Update()
    {
        RaycastChecks();
        RotateObjectMouse();
    }
    #endregion

    #region My Functions

    #region Examine Object
    void RotateObjectMouse()
    {
        if (gmData.currPlayerState == GameManagerData.PlayerState.Examine)
        {
            if (Input.GetMouseButton(0) && _rotateObjTransform != null)
            {
                float horizontal = Input.GetAxis("Mouse X") * rotationSpeedMouse * Time.deltaTime;
                float vertical = Input.GetAxis("Mouse Y") * rotationSpeedMouse * Time.deltaTime;

                _rotateObjTransform.RotateAround(_rotateObjTransform.position, Vector3.right, vertical);
                _rotateObjTransform.RotateAround(_rotateObjTransform.position, Vector3.down, horizontal);
            }
        }
    }

    void RaycastChecks()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        _isInteracting = Physics.Raycast(ray, out _hit, rayDistance, examineLayer);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, _isInteracting ? Color.red : Color.white);

        if (_isInteracting)
        {
            if (Input.GetKeyDown(pickupKey) && !_isExamine)
            {
                ExamineStarted(_hit.collider.gameObject);
                EnableCursor();
                _isExamine = true;
                //Debug.Log("Examine Started");
            }
        }

        if (Input.GetKeyDown(dropKey) && _isExamine)
        {
            ExamineEnded();
            DisableCursor();
            _isExamine = false;
            //Debug.Log("Examine Ended");
        }
    }
    #endregion

    #region Examine Object References
    void ExamineStarted(GameObject obj)
    {
        _tempObjReference = obj;
        _tempObjReference.GetComponent<Collider>().enabled = false;

        _intialObjPos = _tempObjReference.transform.position;
        _intialObjRot = _tempObjReference.transform.rotation;
        _intialObjScale = _tempObjReference.transform.localScale;

        if (_tempObjReference.CompareTag("Hatchet"))
            _tempObjReference.transform.localScale = scaleDown;

        _tempObjReference.layer = LayerMask.NameToLayer("InspectionCamLayer");

        _tempObjReference.transform.parent = examinePointPos;
        _rotateObjTransform = _tempObjReference.transform;
        _tempObjReference.transform.position = examinePointPos.position;
        _tempObjReference.transform.rotation = examinePointPos.rotation;
        gmData.currPlayerState = GameManagerData.PlayerState.Examine;
    }

    void ExamineEnded()
    {
        if (_tempObjReference.CompareTag("Relic") || _tempObjReference.CompareTag("Hatchet"))
            DestoryItem();
        else
            PlaceBackObject();
    }

    void PlaceBackObject()
    {
        _tempObjReference.layer = LayerMask.NameToLayer("ExamineLayer");
        _tempObjReference.transform.parent = pickPropReturnParentPos;
        _tempObjReference.transform.position = _intialObjPos;
        _tempObjReference.transform.rotation = _intialObjRot;
        _tempObjReference.transform.localScale = _intialObjScale;
        _tempObjReference.GetComponent<Collider>().enabled = true;

        _tempObjReference = null;
        _rotateObjTransform = null;

        gmData.currPlayerState = GameManagerData.PlayerState.Moving;
    }

    void DestoryItem()
    {
        Destroy(_tempObjReference);
        _tempObjReference = null;
        _rotateObjTransform = null;
        OnRelicDestroy?.Invoke();

        gmData.currPlayerState = GameManagerData.PlayerState.Moving;
    }
    #endregion

    #region Cursor
    void EnableCursor()
    {
        gmData.VisibleCursor(true);
        gmData.LockCursor(false);
        hudPanel.SetActive(false);
    }

    void DisableCursor()
    {
        gmData.VisibleCursor(false);
        gmData.LockCursor(true);
        hudPanel.SetActive(true);
    }
    #endregion

    #endregion
}