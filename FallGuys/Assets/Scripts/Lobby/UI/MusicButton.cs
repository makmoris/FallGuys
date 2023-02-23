using UnityEngine;
using UnityEngine.UI;

public class MusicButton : MonoBehaviour
{
    [SerializeField] private Image musicOn;
    [SerializeField] private Image musicOff;

    private void Awake()
    {
        bool state = MusicManager.Instance.GetMusicEnabledState();
        SetButtonState(state);
    }

    private void OnEnable()
    {
        bool state = MusicManager.Instance.GetMusicEnabledState();
        SetButtonState(state);
    }

    public void SetMusicEnabled()
    {
        if (musicOn.gameObject.activeSelf) MusicOff();
        else MusicOn();
    }

    private void MusicOn()
    {
        MusicManager.Instance.ToggleMusic(true);
        SetButtonState(true);
    }
    private void MusicOff()
    {
        MusicManager.Instance.ToggleMusic(false);
        SetButtonState(false);
    }

    private void SetButtonState(bool musicEnabled)
    {
        if (musicEnabled)
        {
            musicOn.gameObject.SetActive(true);
            musicOff.gameObject.SetActive(false);
        }
        else
        {
            musicOn.gameObject.SetActive(false);
            musicOff.gameObject.SetActive(true);
        }
    }
}
