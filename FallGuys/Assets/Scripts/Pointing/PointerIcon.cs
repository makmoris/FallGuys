using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerIcon : MonoBehaviour
{
    [SerializeField] Image _image;
    bool _isShown = true;

    [SerializeField]private float startHealthValue;
    [SerializeField]private bool isFirstColorUpdate = true;

    private void Awake()
    {
        _image.enabled = false;
        _isShown = false;
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;
    }

    public void Show()
    {
        if (_isShown) return;
        _isShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide()
    {
        if (!_isShown) return;
        _isShown = false;

        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }

    public void UpdateHealthColor(float healthValue)
    {
        if (isFirstColorUpdate)
        {
            startHealthValue = healthValue;
            _image.color = Color.green;

            isFirstColorUpdate = false;
        }
        else
        {
            float healthPercent = (healthValue / startHealthValue) * 100f;

            if (healthPercent > 65)
            {
                _image.color = Color.green;
            }
            else if (healthPercent <= 65 && healthPercent > 35)
            {
                _image.color = Color.yellow;
            }
            else
            {
                _image.color = Color.red;
            }
        }
    }

    IEnumerator ShowProcess()
    {
        _image.enabled = true;
        transform.localScale = Vector3.zero;
        for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
        {
            transform.localScale = Vector3.one * t;
            yield return null;
        }
        transform.localScale = Vector3.one;
    }

    IEnumerator HideProcess()
    {

        for (float t = 0; t < 1f; t += Time.deltaTime * 4f)
        {
            transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }
        _image.enabled = false;
    }
}
