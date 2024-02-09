using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CupRewardCongratulationsPanel : MonoBehaviour
{
    [SerializeField] private Image _playerItemRewardIcon;
    [Space]
    [SerializeField] private Image _coinsRewardIcon;
    [SerializeField] private TextMeshProUGUI _coinsRewardText;

    public void ShowPlayerItemRewardCongratulations(Sprite playerItemRewardIcon)
    {
        HideAll();

        _playerItemRewardIcon.sprite = playerItemRewardIcon;

        _playerItemRewardIcon.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }

    public void ShowCoinsRewardCongratulations(Sprite coinsRewardIcon, string coinsRewardValueText)
    {
        HideAll();

        _coinsRewardIcon.sprite = coinsRewardIcon;
        _coinsRewardText.text = coinsRewardValueText;

        _coinsRewardIcon.gameObject.SetActive(true);
        _coinsRewardText.gameObject.SetActive(true);

        gameObject.SetActive(true);
    }

    private void HideAll()
    {
        _playerItemRewardIcon.gameObject.SetActive(false);
        _coinsRewardIcon.gameObject.SetActive(false);
        _coinsRewardText.gameObject.SetActive(false);
    }
}
