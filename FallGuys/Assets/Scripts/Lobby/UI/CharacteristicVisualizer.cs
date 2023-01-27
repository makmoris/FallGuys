using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharacteristicType
{
    HP = 0,

    Damage = 1,
    RechargeTime = 2,
    AttackRange = 3
}

public class CharacteristicVisualizer : MonoBehaviour
{
    [Header("Maximum Values Of Characteristics")]
    [SerializeField] private MaximumValuesOfCharacteristics maximumValues;
    [Header("Type Of Characteristic")]
    [SerializeField] private CharacteristicType characteristicType;
    [Space]
    [SerializeField] private TextMeshProUGUI characteristicText;

    private Image _bottomScale;
    private Image _upperScale;

    private float _maxValue;

    private Color blueText = new Color(184f / 255f, 224f / 255f, 255f / 255f);
    private Color green = new Color(125f / 255f, 226f / 255f, 64f / 255f);
    private Color red = new Color(255f / 255f, 108f / 255f, 108f / 255f);
    private Color yellow = new Color(255f / 255f, 222f / 255f, 77f / 255f);

    private void Awake()
    {
        _bottomScale = GetComponent<Image>();
        _upperScale = transform.GetChild(0).GetComponent<Image>();
        _maxValue = GetValueOfType();
    }

    public void SetScaleValue(float value)// вызываетс€ кнопкой, передава€ значени€ из Data
    {
        float activeValue = GetActiveWeaponValueOfType();// значение выбранного (установленного) оружи€

        float currentValuePercent = value / _maxValue;// процент оружи€, которое выбрано дл€ просмотра
        float activeValuePercent = activeValue / _maxValue;// процент оружи€, активного, выбранного на авто

        if(currentValuePercent < activeValuePercent)
        {
            float difference = activeValue - value;

            if(characteristicType == CharacteristicType.RechargeTime)// если врем€ перезар€дки меньше, то это наоборот хорошо
            {
                _bottomScale.fillAmount = activeValuePercent;
                _bottomScale.color = green;

                _upperScale.fillAmount = currentValuePercent;
                _upperScale.color = yellow;

                characteristicText.text = $"- {difference}";
                characteristicText.color = green;
            }
            else
            {
                _bottomScale.fillAmount = activeValuePercent;
                _bottomScale.color = red;

                _upperScale.fillAmount = currentValuePercent;
                _upperScale.color = yellow;

                characteristicText.text = $"- {difference}";
                characteristicText.color = red;
            }
        }
        if (currentValuePercent > activeValuePercent)
        {
            float difference = value - activeValue;

            if (characteristicType == CharacteristicType.RechargeTime)
            {
                _bottomScale.fillAmount = currentValuePercent;
                _bottomScale.color = red;

                _upperScale.fillAmount = activeValuePercent;
                _upperScale.color = yellow;

                characteristicText.text = $"+ {difference}";
                characteristicText.color = red;
            }
            else
            {
                _bottomScale.fillAmount = currentValuePercent;
                _bottomScale.color = green;

                _upperScale.fillAmount = activeValuePercent;
                _upperScale.color = yellow;

                characteristicText.text = $"+ {difference}";
                characteristicText.color = green;
            }
        }
        if (currentValuePercent == activeValuePercent)
        {
            _bottomScale.fillAmount = currentValuePercent;
            _bottomScale.color = yellow;

            _upperScale.fillAmount = activeValuePercent;
            _upperScale.color = yellow;

            characteristicText.text = $"{value}";
            characteristicText.color = blueText;
        }

        //_bottomScale.fillAmount = currentValuePercent;
        //_upperScale.fillAmount = activeValuePercent;
    }

    private float GetValueOfType()
    {
        float value = 0;

        switch (characteristicType)
        {
            case CharacteristicType.HP:
                value = maximumValues.MaxHP;
                break;
            case CharacteristicType.Damage:
                value = maximumValues.MaxDamage;
                break;
            case CharacteristicType.RechargeTime:
                value = maximumValues.MaxRechargeTime;
                break;
            case CharacteristicType.AttackRange:
                value = maximumValues.MaxAttackRange;
                break;
        }

        return value;
    }

    private float GetActiveWeaponValueOfType()
    {
        WeaponCharacteristicsData data = LobbyManager.Instance.GetActiveWeapon().GetComponent<LobbyWeapon>().GetLobbyWeaponData().WeaponDefaultData;

        float value = 0;

        switch (characteristicType)
        {
            case CharacteristicType.Damage:
                value = data.Damage * -1f;
                break;
            case CharacteristicType.RechargeTime:
                value = data.RechargeTime;
                break;
            case CharacteristicType.AttackRange:
                value = data.AttackRange;
                break;
        }

        return value;
    }
}
