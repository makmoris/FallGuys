using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PostLevelUIController : MonoBehaviour
{
    protected GameManager _gameManager;
    public GameManager GameManager { set => _gameManager = value; }

    [SerializeField] private TextMeshProUGUI textWithNumberOfWinners;

    public void SetNumberOfWinnersText(int numberOfWinners)
    {
        textWithNumberOfWinners.text = numberOfWinners.ToString();
    }
}
