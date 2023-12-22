using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public abstract class PointerIcon : MonoBehaviour
{
    [SerializeField] protected Image _image;

    protected bool pointerIsOffScreen;

    private bool _isShown = true;
    private bool notFirstOffScreen;
    private bool notFirstWithinScreen;

    private Vector2 nativePosition;
    private Vector2 nativeImagePosition;

    private Coroutine showProcessCoroutine;
    private Coroutine hideProcessCoroutine;

    private void Awake()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();

        nativePosition = GetComponent<RectTransform>().anchoredPosition;
        nativeImagePosition = _image.GetComponent<RectTransform>().anchoredPosition;

        _image.enabled = false;
        _isShown = false;
    }

    public void OffScreenPointer()
    {
        pointerIsOffScreen = true;

        PointerPositionRelativeToTheScreenHasChanged();

        if (!notFirstOffScreen)
        {
            _image.SetNativeSize();

            notFirstOffScreen = true;
            notFirstWithinScreen = false;
        }
        //_image.SetNativeSize(); // нагружает   
    }
    public void WithinScreenPointer()
    {
        pointerIsOffScreen = false;

        PointerPositionRelativeToTheScreenHasChanged();

        if (!notFirstWithinScreen)
        {
            _image.SetNativeSize();

            notFirstWithinScreen = true;
            notFirstOffScreen = false;
        }
        //_image.SetNativeSize();// нагружает
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
        _isShown = true;

        //StopAllCoroutines();

        if (showProcessCoroutine != null) StopCoroutine(showProcessCoroutine);
        if (hideProcessCoroutine != null) StopCoroutine(hideProcessCoroutine);

        if(gameObject.activeSelf) showProcessCoroutine = StartCoroutine(ShowProcess());
    }

    public void Hide()
    {
        if (!_isShown) return;
        _isShown = false;

        //StopAllCoroutines();

        if (showProcessCoroutine != null) StopCoroutine(showProcessCoroutine);
        if (hideProcessCoroutine != null) StopCoroutine(hideProcessCoroutine);

        if (gameObject.activeSelf) hideProcessCoroutine = StartCoroutine(HideProcess());
    }

    public virtual void PointerPositionRelativeToTheScreenHasChanged() { }
    public virtual void UpdateHealthColor(float healthValue) { }

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

    private void OnDisable()
    {
        if (showProcessCoroutine != null) StopCoroutine(showProcessCoroutine);
        if (hideProcessCoroutine != null) StopCoroutine(hideProcessCoroutine);
    }
}
