using UnityEngine;
using UnityEngine.UI;

public class SoundsButton : MonoBehaviour
{
    [SerializeField] private Image soundsOn;
    [SerializeField] private Image soundsOff;

    private void Awake()
    {
        bool state = MusicManager.Instance.GetSoundsEnabledState();
        SetButtonState(state);
    }

    private void OnEnable()
    {
        bool state = MusicManager.Instance.GetSoundsEnabledState();
        SetButtonState(state);
    }

    public void SetSoundsEnabled()
    {
        if (soundsOn.gameObject.activeSelf) SoundsOff();
        else SoundsOn();
    }

    private void SoundsOn()
    {
        MusicManager.Instance.ToggleSounds(true);
        SetButtonState(true);
    }
    private void SoundsOff()
    {
        MusicManager.Instance.ToggleSounds(false);
        SetButtonState(false);
    }

    private void SetButtonState(bool soundEnabled)
    {
        if (soundEnabled)
        {
            soundsOn.gameObject.SetActive(true);
            soundsOff.gameObject.SetActive(false);
        }
        else
        {
            soundsOn.gameObject.SetActive(false);
            soundsOff.gameObject.SetActive(true);
        }
    }
}
