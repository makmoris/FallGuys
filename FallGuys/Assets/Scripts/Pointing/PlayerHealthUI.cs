using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    private float startHealthValue;
    private bool isFirstHealthUpdate = true;

    private Image _image;
    private TextMeshProUGUI _text;

    private void Awake()
    {
        _image = gameObject.GetComponent<Image>();
        _text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    public void UpdatePlayerHealthUI(float value)
    {
        if (isFirstHealthUpdate)
        {
            startHealthValue = value;
            _image.color = Color.green;
            _image.fillAmount = 1f;
            _text.text = $"{startHealthValue} / {startHealthValue}";

            isFirstHealthUpdate = false;
        }
        else
        {
            float healthPercent = value / startHealthValue;
            _image.fillAmount = healthPercent;

            if (healthPercent > 0.65f)
            {
                _image.color = Color.green;
            }
            else if (healthPercent <= 0.65f && healthPercent > 0.35f)
            {
                _image.color = Color.yellow;
            }
            else
            {
                _image.color = Color.red;
            }

            float val = startHealthValue * healthPercent;
            
            if(healthPercent > 0) _text.text = $"{(int)val} / {startHealthValue}";
            else _text.text = $"0 / {startHealthValue}";
        }
    }
}
