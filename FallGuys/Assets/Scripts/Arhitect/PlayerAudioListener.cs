using UnityEngine;

public class PlayerAudioListener : MonoBehaviour
{
    private AudioListener audioListener;
    private bool blockEnable;
    public bool BlockEnable => blockEnable;

    private void Awake()
    {
        audioListener = GetComponent<AudioListener>();
    }

    public void EnableAudioListener()
    {
        if (!blockEnable) audioListener.enabled = true;
    }

    public void DisableAudioListener()
    {
        audioListener.enabled = false;
    }

    public void BlockEnabling()
    {
        blockEnable = true;
    }
}
