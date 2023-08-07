using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelProgressUIController : MonoBehaviour
{
    [Header("For Game Modes")]
    [SerializeField] protected GameObject defaultUI;
    [SerializeField] protected GameObject additionalUI;
    [SerializeField] protected GameObject leaveButton;

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

    public virtual void ShowPlayerUI()
    {
        defaultUI.SetActive(true);
        additionalUI.SetActive(true);
        leaveButton.SetActive(false);
    }
    public virtual void ShowObserverUI()
    {
        defaultUI.SetActive(false);
        additionalUI.SetActive(true);
        leaveButton.SetActive(true);
    }

    public void ShowCameraHint()
    {
        cameraHint.SetActive(true);
    }

    public void ShowCongratilations()
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
}
