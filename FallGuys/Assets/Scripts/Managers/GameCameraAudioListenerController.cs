using UnityEngine;

public class GameCameraAudioListenerController : MonoBehaviour
{
    private AudioListener audioListener;

    public static GameCameraAudioListenerController Instance { get; private set; }
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        audioListener = GetComponent<AudioListener>();
        DeactivateAudioListener();
    }

    public void ActivateAudioListener()
    {
        audioListener.enabled = true;
    }

    public void DeactivateAudioListener()
    {
        audioListener.enabled = false;
    }
}
