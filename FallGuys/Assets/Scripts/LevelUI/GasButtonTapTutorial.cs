using UnityEngine;
using DG.Tweening;
using AssetKits.ParticleImage;

public class GasButtonTapTutorial : MonoBehaviour
{
    [SerializeField] private ParticleImage _particleImage;

    private Tween _scaleTween;
    private RectTransform _rectTransform;
    private Vector2 _defaulScale;
    private bool isWasHide;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _defaulScale = _rectTransform.localScale;
    }

    private void OnEnable()
    {
        ShowScaleAnimation();
    }

    public void StopTutorial()
    {
        if (!isWasHide)
        {
            _particleImage.gameObject.SetActive(false);
            KillTweens();

            isWasHide = true;
        }
    }

    private void ShowScaleAnimation()
    {
        KillTweens();
        isWasHide = false;

        _particleImage.gameObject.SetActive(true);
        _particleImage.Play();

        _scaleTween = _rectTransform.DOScale(1.2f, 0.25f).SetDelay(0.5f).SetEase(Ease.OutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    private void KillTweens()
    {
        _rectTransform.localScale = _defaulScale;

        if (_scaleTween != null && _scaleTween.IsActive() && !_scaleTween.IsComplete())
        {
            _scaleTween.Kill();
            _scaleTween = null;
        }
    }

    private void OnDisable()
    {
        KillTweens();
    }
}
