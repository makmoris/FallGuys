using System;
using PunchCars.InAppPurchasing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ShopButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private Image _rewardedAdPrice;
    [SerializeField] private Image _coinsPrice;
    [SerializeField] private TextMeshProUGUI _priceText;
    [SerializeField] private TextMeshProUGUI _priceOldText;
    [SerializeField] private TextMeshProUGUI _discountText;
    [SerializeField] private CanvasGroup _discount;
    [SerializeField] private GameObject _oldPrice;
    [SerializeField] private GameObject _separator;

    private const string RewardedAdPriceText = "FREE";

    public event Action OnClicked;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(OnButtonClicked);
    }

    public void ShowDiscount(bool show, float discount, string oldPrice)
    {
        _oldPrice.SetActive(show);
        _discount.gameObject.SetActive(show);
        _discountText.text = $"-{(discount * 100):F0}%";
        _priceOldText.text = oldPrice;
    }

    public void SetPrice(BuyMethode currency, string price)
    {
        switch (currency)
        {
            case BuyMethode.RealMoney:
                _coinsPrice.gameObject.SetActive(false);
                _rewardedAdPrice.gameObject.SetActive(false);
                _separator.SetActive(false);
                _priceText.text = price;
                _priceText.alignment = TextAlignmentOptions.Center;
                break;
            case BuyMethode.Coins:
                _coinsPrice.gameObject.SetActive(true);
                _rewardedAdPrice.gameObject.SetActive(false);
                _priceText.text = price;
                _separator.SetActive(true);
                _priceText.alignment = TextAlignmentOptions.Left;
                break;
            case BuyMethode.RewardedAd:
                _coinsPrice.gameObject.SetActive(false);
                _rewardedAdPrice.gameObject.SetActive(true);
                _separator.SetActive(false);
                _priceText.text = RewardedAdPriceText;
                _priceText.alignment = TextAlignmentOptions.Left;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(currency), currency, null);
        }
    }

    public void SetInteractable(bool interactable)
    {
        _priceText.alpha = interactable ? 1 : 0.5f;
        _button.interactable = interactable;
        _coinsPrice.color = interactable ? Color.white : new Color(1, 1, 1, .5f);
        _rewardedAdPrice.color = interactable ? Color.white : new Color(1, 1, 1, .5f);
        _discount.alpha = interactable ? 1 : 0.5f;
    }

    private void OnButtonClicked()
    {
        OnClicked?.Invoke();
    }
}
