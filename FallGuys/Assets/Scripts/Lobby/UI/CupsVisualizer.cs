using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class CupsVisualizer : MonoBehaviour
{
    private TextMeshProUGUI cupsText;

    private void Awake()
    {
        cupsText = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdateCupsValue(CurrencyManager.Instance.Cups);
    }

    private void UpdateCupsValue(int value)
    {
        cupsText.text = value.ToString();
    }


    private void OnEnable()
    {
        CurrencyManager.CupsUpdateEvent += UpdateCupsValue;
    }

    private void OnDisable()
    {
        CurrencyManager.CupsUpdateEvent -= UpdateCupsValue;
    }
}
