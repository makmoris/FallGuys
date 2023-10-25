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
    [SerializeField] private GameObject WeaponCharacteristicsGO;

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
        if (!weaponScroll.activeSelf)
        {
            WeaponCharacteristicsGO.SetActive(true);
            weaponScroll.SetActive(true);
        }
        if (colorScroll.activeSelf)
        {
            colorScroll.SetActive(false);
        }

        //SetColorButtonActive();
        SetWeaponButtonActive();
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
