using UnityEngine;
using UnityEngine.UI;

public class VibrationButton : MonoBehaviour
{
    [SerializeField] private Image vibrationOn;
    [SerializeField] private Image vibrationOff;

    private void Awake()
    {
        bool state = VibrationManager.Instance.GetVibrationEnabledState();
        SetButtonState(state);
    }

    private void OnEnable()
    {
        bool state = VibrationManager.Instance.GetVibrationEnabledState();
        SetButtonState(state);
    }

    public void SetVibrationEnabled()
    {
        if (vibrationOn.gameObject.activeSelf) VibrationOff();
        else VibrationOn();
    }

    private void VibrationOn()
    {
        VibrationManager.Instance.ToogleVibration(true);
        SetButtonState(true);
    }
    private void VibrationOff()
    {
        VibrationManager.Instance.ToogleVibration(false);
        SetButtonState(false);
    }

    private void SetButtonState(bool vibrationEnabled)
    {
        if (vibrationEnabled)
        {
            vibrationOn.gameObject.SetActive(true);
            vibrationOff.gameObject.SetActive(false);
        }
        else
        {
            vibrationOn.gameObject.SetActive(false);
            vibrationOff.gameObject.SetActive(true);
        }
    }
}
