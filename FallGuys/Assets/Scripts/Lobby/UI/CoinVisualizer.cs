using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CoinVisualizer : MonoBehaviour
{
    private TextMeshProUGUI coinText;

    private void Awake()
    {
        coinText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateCoinsValue(CurrencyManager.Instance.Gold);
    }

    private void UpdateCoinsValue(int value)
    {
        coinText.text = value.ToString();
    }


    private void OnEnable()
    {
        CurrencyManager.GoldUpdateEvent += UpdateCoinsValue;
    }

    private void OnDisable()
    {
        CurrencyManager.GoldUpdateEvent -= UpdateCoinsValue;
    }
}
