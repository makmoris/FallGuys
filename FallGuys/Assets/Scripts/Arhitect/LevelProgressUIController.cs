using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class LevelProgressUIController : MonoBehaviour
{
    protected GameManager _gameManager;

    [Header("For Game Modes")]
    [SerializeField] protected GameObject defaultUI;
    [SerializeField] protected GameObject additionalUI;
    [SerializeField] protected Button leaveButton;

    [Header("Congratulations And Lose Panels")]
    [SerializeField] private GameObject congratulationsPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private float showingTime = 2f;

    public event System.Action CongratulationsOverEvent;
    public event System.Action LoseShowingOverEvent;

    [Space]
    [SerializeField] private GameObject layouts;

    [Header("Camera Hint")]
    [SerializeField] private GameObject cameraHint;

    private void Awake()
    {
        HideElementsBeforeStart();
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;

        SetLeaveButtonEvent();
    }

    public virtual void ShowPlayerUI()
    {
        defaultUI.SetActive(true);
        additionalUI.SetActive(true);
        leaveButton.gameObject.SetActive(false);
    }
    public virtual void ShowObserverUI()
    {
        defaultUI.SetActive(false);
        additionalUI.SetActive(true);
        leaveButton.gameObject.SetActive(true);

        ShowCameraHint();
    }

    public void ShowCameraHint()
    {
        cameraHint.SetActive(true);
    }

    public void HideCameraHint()
    {
        cameraHint.SetActive(false);
    }

    public void ShowCongratilationsPanel()
    {
        layouts.SetActive(false);
        congratulationsPanel.SetActive(true);
        StartCoroutine(WaitAndHideCongratulationsPanel(showingTime));
    }

    IEnumerator WaitAndHideCongratulationsPanel(float congratulationsTime)
    {
        yield return new WaitForSeconds(congratulationsTime);
        congratulationsPanel.SetActive(false);

        CongratulationsOverEvent?.Invoke();
    }

    public void ShowLosingPanel()
    {
        layouts.SetActive(false);
        losePanel.SetActive(true);
        StartCoroutine(WaitAndHideLosingPanel(showingTime));
    }

    IEnumerator WaitAndHideLosingPanel(float congratulationsTime)
    {
        yield return new WaitForSeconds(congratulationsTime);
        losePanel.SetActive(false);

        LoseShowingOverEvent?.Invoke();
    }

    protected virtual void HideElementsBeforeStart()
    {
        if (congratulationsPanel.activeSelf) congratulationsPanel.SetActive(false);
        if (losePanel.activeSelf) losePanel.SetActive(false);
        if (cameraHint.activeSelf) cameraHint.SetActive(false);

        if (!layouts.activeSelf) layouts.SetActive(true);
    }

    private void SetLeaveButtonEvent()
    {
        leaveButton.onClick.RemoveAllListeners();
        leaveButton.onClick.AddListener(_gameManager.PlayerClickedExitToLobby);
        leaveButton.onClick.AddListener(DeactivateLeaveButton);
    }

    private void DeactivateLeaveButton()
    {
        leaveButton.interactable = false;
    }
}
