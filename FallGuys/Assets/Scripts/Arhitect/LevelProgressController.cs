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


    public void SetNumberOfPlayers(int _numberOfPlayers)// ��������� �����
    {
        numberOfPlayers = _numberOfPlayers;
        numberOfFrags = 0;

        UpdateLeftText();
        UpdateFragText();
    }

    public void AddFrag()// ���������� �� VisualIntermediary
    {
        numberOfFrags++;

        amountOfGoldReward += LeagueManager.Instance.GetGoldRewardForFrag();
        amountOfCupReward += LeagueManager.Instance.GetCupRewardForFrag();

        UpdateFragText();
    }

    public void AddGold(int value)// ���������� �� PlayerEffector �� ������ ������
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
            // ������ ���� ���������. ���� ���������
            Debug.Log($"GameOver. ����� {numberOfPlayers + 1} �����");

            CalculateReward(numberOfPlayers + 1);
        }
        else if (numberOfPlayers == 1)
        {
            // ���� ������� ���� �����, �.�. numberOfPlayers = 1, �� ������ ���� ������
            Debug.Log($"Win. ����� {numberOfPlayers} �����");

            CalculateReward(numberOfPlayers);
        }
    }
    
    private void CalculateReward(int place)
    {
        amountOfGoldReward += LeagueManager.Instance.ReceiveGoldsAsReward(place);
        amountOfCupReward += LeagueManager.Instance.ReceiveCupsAsReward(place);

        Debug.Log($"�������: ������ - {amountOfGoldReward}; ����� - {amountOfCupReward}");

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
