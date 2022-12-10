using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    
    private Image _image;

    [SerializeField]private float _maxValue;

    private void Awake()
    {
        _image = GetComponent<Image>();
        _maxValue = GetValueOfType();
    }

    public void SetScaleValue(float value)// вызывается кнопкой, передавая значения из Data
    {
        float valuePercent = value / _maxValue;
        _image.fillAmount = valuePercent;
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
}
