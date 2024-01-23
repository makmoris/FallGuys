using UnityEngine;

public class ShowCutScene : MonoBehaviour
{
    [Header("Will there be a cutscene?")]
    [SerializeField] private bool haveCutscene;
    [Space]
    [SerializeField] private GameObject showTrackCutSceneCameras;
    [SerializeField] private Canvas gameCanvas;// его тоже пр€чем

    public event System.Action ShowTrackCutSceneEndedEvent;

    private void Awake()
    {
        if (haveCutscene)
        {
            if (gameCanvas.gameObject.activeSelf) gameCanvas.gameObject.SetActive(false);
            if (!showTrackCutSceneCameras.activeSelf) showTrackCutSceneCameras.SetActive(true);
        }
        else
        {
            if (!gameCanvas.gameObject.activeSelf) gameCanvas.gameObject.SetActive(true);
            if (showTrackCutSceneCameras.activeSelf) showTrackCutSceneCameras.SetActive(false);
        }
    }

    private void Start()
    {
        MusicManager.Instance.TurnOffSoundsTemporarily();

        if (!haveCutscene)
        {
            ShowTrackCutSceneEndedEvent?.Invoke();
            MusicManager.Instance.TurnOnSoundsTemporarily();
        }
    }

    public void CutSceneEnded()
    {
        Debug.Log("Cut Scene Ended");
        
        showTrackCutSceneCameras.SetActive(false);
        gameCanvas.gameObject.SetActive(true);

        MusicManager.Instance.TurnOnSoundsTemporarily();

        ShowTrackCutSceneEndedEvent?.Invoke();
    }
}
