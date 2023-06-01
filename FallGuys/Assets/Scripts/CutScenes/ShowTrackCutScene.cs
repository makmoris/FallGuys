using UnityEngine;

public class ShowTrackCutScene : MonoBehaviour
{
    [SerializeField] private GameObject showTrackCutSceneCameras;
    [SerializeField] private Canvas gameCanvas;// его тоже пр€чем

    public event System.Action ShowTrackCutSceneEndedEvent;

    private void Awake()
    {
        if (gameCanvas.gameObject.activeSelf) gameCanvas.gameObject.SetActive(false);
        if (!showTrackCutSceneCameras.activeSelf) showTrackCutSceneCameras.SetActive(true);
    }

    private void Start()
    {
        MusicManager.Instance.StopSoundsPlaying();
    }

    public void CutSceneEnded()
    {
        Debug.Log("Cut Scene Ended");
        
        showTrackCutSceneCameras.SetActive(false);
        gameCanvas.gameObject.SetActive(true);

        MusicManager.Instance.ReturnPreviousSoundsValue();

        ShowTrackCutSceneEndedEvent?.Invoke();
    }
}
