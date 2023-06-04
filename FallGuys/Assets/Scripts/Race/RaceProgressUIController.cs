using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceProgressUIController : MonoBehaviour
{
    [Header("Cut Scene")]
    [SerializeField] private ShowTrackCutScene trackCutScene;

    [Header("Start")]
    [SerializeField] private GameObject startRaceUIPanel;
    [SerializeField] private TimerBeforeStart timerBeforeStart;
    [SerializeField] private int time = 3;

    [Header("Finish")]
    [SerializeField] private RacePassedPanel winnersPanel;
    [Space]
    [SerializeField] private GameObject congratulationsPanel;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private float showingTime = 2f;

    public event System.Action RaceCanStartEvent;

    public event System.Action CongratulationsOverEvent;
    public event System.Action LoseShowingOverEvent;

    private void OnEnable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent += StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent += TimerBeforeStartFinished;
    }

    private void Awake()
    {
        if (!startRaceUIPanel.activeSelf) startRaceUIPanel.SetActive(true);
        if (winnersPanel.gameObject.activeSelf) winnersPanel.gameObject.SetActive(false);
        if (congratulationsPanel.activeSelf) congratulationsPanel.SetActive(false);
        if (losePanel.activeSelf) losePanel.SetActive(false);
    }

    public void ShowPostRacingWindow()
    {

    }

    public void ShowPlayerCongratulations()
    {
        congratulationsPanel.SetActive(true);
        StartCoroutine(WaitAndHideCongratulationsPanel(showingTime));
    }

    public void ShowPlayerLose()
    {
        losePanel.SetActive(true);
        StartCoroutine(WaitAndHideLosePanel(showingTime));
    }

    public void SetNumberOfWinners(int numberOfWinners)
    {
        winnersPanel.SetNumberOfWinners(numberOfWinners);
    }

    public void UpdateNumberOfWinners(int currentNumber)
    {
        winnersPanel.UpdateNamberOfWinners(currentNumber);
    }

    private void StartShowingGoalAndTimer()
    {
        timerBeforeStart.StartTimer(time);
    }

    private void TimerBeforeStartFinished()
    {
        startRaceUIPanel.SetActive(false);
        winnersPanel.gameObject.SetActive(true);

        RaceCanStartEvent?.Invoke();
    }

    IEnumerator WaitAndHideCongratulationsPanel(float congratulationsTime)
    {
        yield return new WaitForSeconds(congratulationsTime);
        congratulationsPanel.SetActive(false);

        CongratulationsOverEvent?.Invoke();
    }

    IEnumerator WaitAndHideLosePanel(float congratulationsTime)
    {
        yield return new WaitForSeconds(congratulationsTime);
        losePanel.SetActive(false);

        LoseShowingOverEvent?.Invoke();
    }

    private void OnDisable()
    {
        trackCutScene.ShowTrackCutSceneEndedEvent -= StartShowingGoalAndTimer;
        timerBeforeStart.TimerBeforeStartFinishedEvent -= TimerBeforeStartFinished;
    }
}
