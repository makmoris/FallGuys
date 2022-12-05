using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointerIcon : MonoBehaviour
{
    [SerializeField] private Image _image;

    [Header("Prefabs")]
    [SerializeField] private Sprite greenPointerWithinScreen;
    [SerializeField] private Sprite yellowPointerWithinScreen;
    [SerializeField] private Sprite redPointerWithinScreen;

    [SerializeField] private Sprite greenPointerOffScreen;
    [SerializeField] private Sprite yellowPointerOffScreen;
    [SerializeField] private Sprite redPointerOffScreen;

    bool _isShown = true;

    [Space]
    [SerializeField]private float startHealthValue;
    [SerializeField] private float _healthValue;
    private bool isFirstColorUpdate = true;

    private bool pointerIsOffScreen;

    private Vector2 nativePosition;
    private Vector2 nativeImagePosition;

    private void Awake()
    {
        nativePosition = GetComponent<RectTransform>().anchoredPosition;
        nativeImagePosition = _image.GetComponent<RectTransform>().anchoredPosition;

        _image.enabled = false;
        _isShown = false;
    }

    public void OffScreenPointer()
    {
        pointerIsOffScreen = true;

        UpdateHealthColor(_healthValue);
        _image.SetNativeSize();
    }
    public void WithinScreenPointer()
    {
        pointerIsOffScreen = false;

        UpdateHealthColor(_healthValue);
        _image.SetNativeSize();
    }

    public void SetIconPosition(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        transform.rotation = rotation;

        _image.transform.localPosition = Vector2.zero;
    }
    public void SetIconPosition(Quaternion rotation)
    {
        transform.localPosition = nativePosition;
        transform.rotation = rotation;

        _image.transform.localPosition = nativeImagePosition;
    }

    public void Show()
    {
        if (_isShown) return;
        Debug.Log("Show");
        _isShown = true;
        StopAllCoroutines();
        StartCoroutine(ShowProcess());
    }

    public void Hide()
    {
        if (!_isShown) return;
        Debug.Log("Hide");
        _isShown = false;

        StopAllCoroutines();
        StartCoroutine(HideProcess());
    }

    public void UpdateHealthColor(float healthValue)
    {
        if (isFirstColorUpdate)
        {
            startHealthValue = healthValue;

            if (!pointerIsOffScreen) _image.sprite = greenPointerWithinScreen;
            else _image.sprite = greenPointerOffScreen;

            isFirstColorUpdate = false;

            _healthValue = startHealthValue;
        }
        else
        {
            float healthPercent = (healthValue / startHealthValue) * 100f;

            if (healthPercent > 65)
            {
                if (!pointerIsOffScreen) _image.sprite = greenPointerWithinScreen;
                else _image.sprite = greenPointerOffScreen;
            }
            else if (healthPercent <= 65 && healthPercent > 35)
            {
                if (!pointerIsOffScreen) _image.sprite = yellowPointerWithinScreen;
                else _image.sprite = yellowPointerOffScreen;
            }
            else
            {
                if (!pointerIsOffScreen) _image.sprite = redPointerWithinScreen;
                else _image.sprite = redPointerOffScreen;
            }

            _healthValue = healthPercent;
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
