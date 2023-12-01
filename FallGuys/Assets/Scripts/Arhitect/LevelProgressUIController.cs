using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VehicleBehaviour;

public abstract class LevelProgressUIController : MonoBehaviour
{
    protected GameManager _gameManager;

    [Header("For Game Modes")]
    [SerializeField] protected GameObject defaultUI;
    [SerializeField] protected GameObject additionalUI;
    [SerializeField] protected Button leaveButtonInLevelCanvas;

    [Header("Congratulations And Lose Panels")]
    [SerializeField] private GameObject congratulationsPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private float showingTime = 2f;

    public event System.Action CongratulationsOverEvent;
    public event System.Action LoseShowingOverEvent;

    [Header("Settings Windows")]
    [SerializeField] private GameObject layouts;
    [Space]
    [SerializeField] private Button leaveButtonInSettingsWindow;

    [Header("Camera Hint")]
    [SerializeField] private GameObject cameraHint;

    [Header("Cut Scene")]
    [SerializeField] private ShowCutScene trackCutScene;

    [Header("Start Timer And Goal")]
    [SerializeField] private GameObject goalPanel;
    [SerializeField] private TimerBeforeStart timerBeforeStart;
    [SerializeField] private int time = 3;

    [Header("Respawn Button")]
    [SerializeField] private Button respawnButton;
    private WheelVehicle playerWheelVehicle;

    public event System.Action GameCanStartEvent;

    private void Awake()
    {
        HideElementsBeforeStart();
    }

    protected virtual void OnEnable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent += StartShowingTimerAndShowGoal;
    }

    protected void StartShowingTimerAndShowGoal()
    {
        timerBeforeStart.TimerBeforeStartFinishedEvent += TimerBeforeStartFinished;
        timerBeforeStart.StartTimer(time);
    }

    protected virtual void TimerBeforeStartFinished()
    {
        timerBeforeStart.TimerBeforeStartFinishedEvent -= TimerBeforeStartFinished;

        GameCanStartEvent?.Invoke();

        goalPanel.SetActive(false);
        timerBeforeStart.gameObject.SetActive(false);
    }

    public void SetGameManager(GameManager gameManager)
    {
        _gameManager = gameManager;

        SetLeaveButtonInLevelCanvasEvent();
        SetLeaveButtonInSettingsWindowEvent();
    }

    public virtual void ShowPlayerUI()
    {
        defaultUI.SetActive(true);
        additionalUI.SetActive(true);
        leaveButtonInLevelCanvas.gameObject.SetActive(false);
    }
    public virtual void ShowObserverUI()
    {
        defaultUI.SetActive(false);
        additionalUI.SetActive(true);
        leaveButtonInLevelCanvas.gameObject.SetActive(true);

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

    public void ShowRespawnButton(WheelVehicle playerWV)
    {
        playerWheelVehicle = playerWV;

        respawnButton.enabled = true;
        respawnButton.gameObject.SetActive(true);

        respawnButton.onClick.RemoveAllListeners();
        respawnButton.onClick.AddListener(RespawnPlayer);
    }

    public void HideRespawnButton()
    {
        respawnButton.enabled = false;
        respawnButton.gameObject.SetActive(false);
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
        if (respawnButton.gameObject.activeSelf) respawnButton.gameObject.SetActive(false);

        if (!layouts.activeSelf) layouts.SetActive(true);
        if (!goalPanel.activeSelf) goalPanel.SetActive(true);
    }

    private void RespawnPlayer()
    {
        playerWheelVehicle.RespawnPlayerAfterStuckFromButton();

        HideRespawnButton();
    }

    private void SetLeaveButtonInLevelCanvasEvent()
    {
        leaveButtonInLevelCanvas.onClick.RemoveAllListeners();
        leaveButtonInLevelCanvas.onClick.AddListener(_gameManager.PlayerClickedExitToLobby);
        leaveButtonInLevelCanvas.onClick.AddListener(DeactivateLeaveButtonInLevelCanvas);
    }

    private void DeactivateLeaveButtonInLevelCanvas()
    {
        leaveButtonInLevelCanvas.interactable = false;
    }

    private void SetLeaveButtonInSettingsWindowEvent()
    {
        leaveButtonInSettingsWindow.onClick.RemoveAllListeners();
        leaveButtonInSettingsWindow.onClick.AddListener(_gameManager.PlayerClickedExitToLobbyFromSettingsWindow);
        leaveButtonInSettingsWindow.onClick.AddListener(DeactivateLeaveButtonInSettingsWindow);
    }

    private void DeactivateLeaveButtonInSettingsWindow()
    {
        leaveButtonInSettingsWindow.interactable = false;
    }

    protected virtual void OnDisable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent -= StartShowingTimerAndShowGoal;
    }
}
