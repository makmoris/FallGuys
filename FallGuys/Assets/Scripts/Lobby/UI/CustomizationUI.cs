using UnityEngine;
using UnityEngine.UI;

public class CustomizationUI : MonoBehaviour
{
    [SerializeField] private Sprite activeButtonImage;
    [SerializeField] private Sprite deactiveButtonImage;
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
        colorButton.sprite = activeButtonImage;
        weaponButton.sprite = deactiveButtonImage;
    }

    public void SetWeaponButtonActive()
    {
        colorButton.sprite = deactiveButtonImage;
        weaponButton.sprite = activeButtonImage;
    }
}
