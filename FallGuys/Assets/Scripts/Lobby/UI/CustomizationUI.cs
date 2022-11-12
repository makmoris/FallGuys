using UnityEngine;
using UnityEngine.UI;

public class CustomizationUI : MonoBehaviour
{
    [SerializeField] private Color activeButtonColor;
    [SerializeField] private Color deactiveButtonColor;
    [SerializeField] private Image colorButton;
    [SerializeField] private Image weaponButton;

    [Space]
    [SerializeField] private GameObject colorScroll;
    [SerializeField] private GameObject weaponScroll;

    private bool notFirstActive;

    private void OnEnable()
    {
        if (notFirstActive)
        {
            ScrollsActivation();
        }
    }

    private void Start()
    {
        ScrollsActivation();
        notFirstActive = true;
    }

    private void ScrollsActivation()
    {
        if (!colorScroll.activeSelf) colorScroll.SetActive(true);
        if (weaponScroll.activeSelf) weaponScroll.SetActive(false);

        SetColorButtonActive();
    }

    public void SetColorButtonActive()
    {
        colorButton.color = activeButtonColor;
        weaponButton.color = deactiveButtonColor;
    }

    public void SetWeaponButtonActive()
    {
        colorButton.color = deactiveButtonColor;
        weaponButton.color = activeButtonColor;
    }
}
