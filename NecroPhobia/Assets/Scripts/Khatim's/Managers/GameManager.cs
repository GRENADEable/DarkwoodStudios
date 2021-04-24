using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region Public Variables

    [Space, Header("Data")]
    public GameManagerData gmData;

    [Space, Header("HUD References")]
    public GameObject relicPickupTxt;
    public GameObject hatchetIcon;
    public TextMeshProUGUI relicCountText;

    [Space, Header("Scripted Events")]
    public Animator choppingTree;
    public GameObject tsuchigomoEnemy;
    public GameObject[] rockDoors;

    [Space, Header("Audios")]
    public AudioSource audOneShotSFX;
    public AudioClip[] clips;
    #endregion

    #region Private Variables
    [SerializeReference] private int _currRelicCount = 0;
    #endregion

    #region Unity Callbacks

    #region Events
    void OnEnable()
    {
        ExamineSystem.OnRelicDestroy += OnRelicDestroyEventReceived;
        ExamineSystem.OnHatchetDestroy += OnHatchetDestroyEventReceived;
        ExamineSystem.OnPaperPicked += OnPaperPickedEventReceived;

        PlayerControllerV2.OnTreeChop += OnTreeChopEventReceived;
    }

    void OnDisable()
    {
        ExamineSystem.OnRelicDestroy -= OnRelicDestroyEventReceived;
        ExamineSystem.OnHatchetDestroy -= OnHatchetDestroyEventReceived;
        ExamineSystem.OnPaperPicked -= OnPaperPickedEventReceived;

        PlayerControllerV2.OnTreeChop -= OnTreeChopEventReceived;
    }

    void OnDestroy()
    {
        ExamineSystem.OnRelicDestroy -= OnRelicDestroyEventReceived;
        ExamineSystem.OnHatchetDestroy -= OnHatchetDestroyEventReceived;
        ExamineSystem.OnPaperPicked -= OnPaperPickedEventReceived;

        PlayerControllerV2.OnTreeChop -= OnTreeChopEventReceived;
    }
    #endregion


    void Start()
    {
        relicCountText.text = $"Relic Count: {_currRelicCount}";
        DisableCursor();
    }

    void Update()
    {

    }
    #endregion

    #region My Functions

    #region Scripted Events
    void OpenCave()
    {
        for (int i = 0; i < rockDoors.Length; i++)
            rockDoors[i].SetActive(false);

        audOneShotSFX.PlayOneShot(clips[3], 0.8f);
    }

    void SpawnTsuchigomo()
    {
        rockDoors[0].SetActive(true);
        rockDoors[2].SetActive(true);

        tsuchigomoEnemy.SetActive(true);

        audOneShotSFX.PlayOneShot(clips[3], 0.8f);
    }
    #endregion

    #region Cursor
    void EnableCursor()
    {
        gmData.VisibleCursor(true);
        gmData.LockCursor(false);
    }

    void DisableCursor()
    {
        gmData.VisibleCursor(false);
        gmData.LockCursor(true);
    }
    #endregion

    #endregion

    #region Events
    void OnPaperPickedEventReceived()
    {
        audOneShotSFX.PlayOneShot(clips[1]);
    }

    void OnRelicDestroyEventReceived()
    {
        _currRelicCount++;
        relicCountText.text = $"Relic Count: {_currRelicCount}";
        audOneShotSFX.PlayOneShot(clips[0]);

        if (_currRelicCount == 5)
            OpenCave();

        if (_currRelicCount == 6)
            SpawnTsuchigomo();
    }

    void OnHatchetDestroyEventReceived()
    {
        hatchetIcon.SetActive(true);
    }

    void OnTreeChopEventReceived()
    {
        choppingTree.Play("TreeFallAnim");
        hatchetIcon.SetActive(false);
        audOneShotSFX.PlayOneShot(clips[2]);
    }
    #endregion
}