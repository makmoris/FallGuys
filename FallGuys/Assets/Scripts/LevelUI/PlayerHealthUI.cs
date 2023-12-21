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

    private int currentHealthValue;

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
            _image.color = new Color(98f / 255f, 173f / 255f, 86f / 255f);
            _image.fillAmount = 1f;
            _text.text = $"+ {startHealthValue}";

            currentHealthValue = (int)startHealthValue;

            isFirstHealthUpdate = false;
        }
        else
        {
            float healthPercent = value / startHealthValue;
            _image.fillAmount = healthPercent;

            if (healthPercent > 0.65f)
            {
                _image.color = new Color(98f / 255f, 173f / 255f, 86f / 255f);
            }
            else if (healthPercent <= 0.65f && healthPercent > 0.35f)
            {
                _image.color = new Color(255f / 255f, 225f / 255f, 123f / 255f);
            }
            else
            {
                _image.color = new Color(225f / 255f, 91f / 255f, 82f / 255f);
            }

            float val = startHealthValue * healthPercent;

            if (healthPercent > 0)
            {
                currentHealthValue = (int)val;

                if (currentHealthValue > 0 && currentHealthValue < 1)
                {
                    _text.text = $"+ 1";
                }
                else
                {
                    _text.text = $"+ {(int)val}";
                }
            }
            else _text.text = $"+ 0";
        }
    }

    public int GetHealthValue()
    {
        return currentHealthValue;
    }
}
