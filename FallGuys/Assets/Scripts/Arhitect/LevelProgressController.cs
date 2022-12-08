using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI fragText;

    private int numberOfPlayers;
    private int numberOfFrags;

    [SerializeField]private int amountOfGoldReward;
    [SerializeField]private int amountOfCupReward;

    private GameObject playerGO;

    public static LevelProgressController Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    public void SetNumberOfPlayers(int _numberOfPlayers)// стартовая точка
    {
        numberOfPlayers = _numberOfPlayers;
        numberOfFrags = 0;

        UpdateLeftText();
        UpdateFragText();
    }

    public void AddFrag()// вызывается из VisualIntermediary
    {
        numberOfFrags++;

        amountOfGoldReward += LeagueManager.Instance.GetGoldRewardForFrag();
        amountOfCupReward += LeagueManager.Instance.GetCupRewardForFrag();

        UpdateFragText();
    }

    public void AddGold(int value)// вызывается из PlayerEffector за подбор бонуса
    {
        amountOfGoldReward += value;
    }

    private void ReduceNumberOfPlayers(GameObject deadPlayer)
    {
        numberOfPlayers--;
        if (numberOfPlayers < 0) numberOfPlayers = 0;

        UpdateLeftText();

        if(deadPlayer == playerGO)
        {
            // значит окно поражения. Игра закончена
            Debug.Log($"GameOver. Занял {numberOfPlayers + 1} место");

            CalculateReward(numberOfPlayers + 1);
        }
        else if (numberOfPlayers == 1)
        {
            // если остался один игрок, т.е. numberOfPlayers = 1, то кидаем окно победы
            Debug.Log($"Win. Занял {numberOfPlayers} место");

            CalculateReward(numberOfPlayers);
        }
    }
    
    private void CalculateReward(int place)
    {
        amountOfGoldReward += LeagueManager.Instance.ReceiveGoldsAsReward(place);
        amountOfCupReward += LeagueManager.Instance.ReceiveCupsAsReward(place);

        Debug.Log($"Награда: Золото - {amountOfGoldReward}; Кубки - {amountOfCupReward}");

        CurrencyManager.Instance.AddGold(amountOfGoldReward);
        CurrencyManager.Instance.AddCup(amountOfCupReward);
    }

    private void SetCurrentPlayer(GameObject playerObj)
    {
        playerGO = playerObj;
    }

    private void UpdateLeftText()
    {
        leftText.text = $"LEFT: {numberOfPlayers}";
    }

    private void UpdateFragText()
    {
        fragText.text = $"{numberOfFrags}";
    }

    private void OnEnable()
    {
        Installer.IsCurrentPlayer += SetCurrentPlayer;
        VisualIntermediary.PlayerWasDeadEvent += ReduceNumberOfPlayers;
    }
    private void OnDisable()
    {
        Installer.IsCurrentPlayer -= SetCurrentPlayer;
        VisualIntermediary.PlayerWasDeadEvent -= ReduceNumberOfPlayers;
    }
}
