using System;
using System.Globalization;
using PunchCars.InAppPurchasing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinsShopItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _amountText;
    [SerializeField] private Image _icon;
    [SerializeField] private ShopButton _buyButton;
    [SerializeField] private GameObject _mostPopular;
    [SerializeField] private GameObject _bestValue;

    private Action _buyButtonClick;

    private void OnEnable()
    {
        _buyButton.OnClicked += OnBuyBtnClick;
    }

    private void OnDisable()
    {
        _buyButton.OnClicked -= OnBuyBtnClick;
    }

    public void SetBuyClickCallback(Action onBuyClick)
    {
        _buyButtonClick = onBuyClick;
    }

    public void SetPrice(BuyMethode currency, string price) => _buyButton.SetPrice(currency, price);

    public void SetIcon(Sprite icon) => _icon.sprite = icon;

    public void SetAmount(int amount)
    {
        var culture = new CultureInfo("ru-RU");

        _amountText.text = amount.ToString("#,#", culture);
    }

    public void SetMostPopularLabel(bool shown) => _mostPopular.SetActive(shown);

    public void SetBestValueLabel(bool shown) => _bestValue.SetActive(shown);

    public void SetButtonInteractable(bool interactable) => _buyButton.SetInteractable(interactable);

    private void OnBuyBtnClick()
    {
        _buyButtonClick?.Invoke();
    }
}
