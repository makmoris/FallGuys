using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VibrationManager : MonoBehaviour
{
    private bool isMobile;

    [SerializeField] private HapticTypes uIButtonsVibrationType = HapticTypes.MediumImpact;
    [SerializeField] private HapticTypes bonusBoxVibrationType = HapticTypes.SoftImpact;
    [Header("Damage Vibration")]
    [SerializeField] private HapticTypes lowDamageVibrationType = HapticTypes.HeavyImpact;
    [SerializeField] private HapticTypes mediumDamageVibrationType = HapticTypes.Warning;
    [SerializeField] private HapticTypes largeDamageVibrationType = HapticTypes.Failure;

    public static VibrationManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        isMobile = Application.isMobilePlatform;
    }

    private void Update()
    {
        if (isMobile)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    var currentObj = EventSystem.current.currentSelectedGameObject;

                    if (currentObj != null && !currentObj.CompareTag("UI_IgnoreVibration"))
                    {
                        Button button = currentObj.GetComponent<Button>();
                        if (button != null)
                        {
                            UIButtonsVibration();
                        }
                    }
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
            {
                var currentObj = EventSystem.current.currentSelectedGameObject;

                if (currentObj != null && !currentObj.CompareTag("UI_IgnoreVibration"))
                {
                    Button button = currentObj.GetComponent<Button>();
                    if (button != null)
                    {
                        Debug.Log("UIButtonVibration");
                        //UIButtonsVibration();
                    }
                }
            }
        }
    }

    public void LowDamageVibration()
    {
        Debug.Log("LowDamageVibration");
        MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void MediumDamageVibration()
    {
        Debug.Log("MediumDamageVibration");
        MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void LargeDamageVibration()
    {
        Debug.Log("LargeDamageVibration");
        MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void BonusBoxVibration()
    {
        Debug.Log("BonusBoxVibration");
        MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    private void UIButtonsVibration()
    {
        MMVibrationManager.Haptic(uIButtonsVibrationType, false, true, this);
    }
}
