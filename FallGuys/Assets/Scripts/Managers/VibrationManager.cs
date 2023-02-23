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

    private bool vibrationEnabled;

    private string  vibrationKey = "VibrationEnabled";

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

        Load();
    }

    public bool GetVibrationEnabledState()
    {
        return vibrationEnabled;
    }

    public void ToogleVibration(bool enable)
    {
        vibrationEnabled = enable;
        Save();
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
                            if (button.gameObject.name == "VibrationButton")
                            {
                                if (vibrationEnabled)
                                {
                                    ToogleVibration(false);
                                }
                                else
                                {
                                    ToogleVibration(true);
                                }
                            }

                            if (vibrationEnabled) UIButtonsVibration();

                            MusicManager.Instance.PlayUIButtonSounds(button);
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
                        if (button.gameObject.name == "VibrationButton")
                        {
                            if (vibrationEnabled)
                            {
                                ToogleVibration(false);
                            }
                            else
                            {
                                ToogleVibration(true);
                            }
                        }

                        if(vibrationEnabled) Debug.Log("UIButtonVibration");

                        MusicManager.Instance.PlayUIButtonSounds(button);
                        //UIButtonsVibration();
                    }
                }
            }
        }
    }

    public void LowDamageVibration()
    {
        Debug.Log("LowDamageVibration");
        if (vibrationEnabled) MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void MediumDamageVibration()
    {
        Debug.Log("MediumDamageVibration");
        if (vibrationEnabled) MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void LargeDamageVibration()
    {
        Debug.Log("LargeDamageVibration");
        if (vibrationEnabled) MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    public void BonusBoxVibration()
    {
        Debug.Log("BonusBoxVibration");
        if (vibrationEnabled) MMVibrationManager.Haptic(bonusBoxVibrationType, false, true, this);
    }

    private void UIButtonsVibration()
    {
        if (vibrationEnabled) MMVibrationManager.Haptic(uIButtonsVibrationType, false, true, this);
    }

    private void Load()
    {
        int vibrationValue = PlayerPrefs.GetInt(vibrationKey, 1);

        if (vibrationValue == 0) vibrationEnabled = false;
        else vibrationEnabled = true;
    }
    private void Save()
    {
        int vibrationValue;

        if (vibrationEnabled) vibrationValue = 1;
        else vibrationValue = 0;

        PlayerPrefs.SetInt(vibrationKey, vibrationValue);
    }
}
