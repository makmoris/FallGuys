using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using VehicleBehaviour;

public class LevelProgressController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI leftText;
    [SerializeField] private TextMeshProUGUI fragText;

    private int numberOfPlayers;
    private int numberOfFrags;

    [SerializeField]private int amountOfGoldReward;
    [SerializeField]private int amountOfCupReward;

    [Header("Win/Lose")]
    [SerializeField] Camera gameCamera;
    [SerializeField] Camera endGameCamera;
    [Header("Win window")]
    [SerializeField] private float pauseBeforShowingWinWindow;
    [SerializeField] private GameObject winWindow;
    [SerializeField] private TextMeshProUGUI fragWWText;
    [SerializeField] private TextMeshProUGUI goldWWText;
    [SerializeField] private TextMeshProUGUI cupsWWText;
    [Header("Lose Window")]
    [SerializeField] private float pauseBeforShowingLoseWindow;
    [SerializeField] private GameObject loseWindow;
    [SerializeField] private TextMeshProUGUI goldLWText;


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

        if (!gameCamera.gameObject.activeSelf) gameCamera.gameObject.SetActive(true);
        if (endGameCamera.gameObject.activeSelf) endGameCamera.gameObject.SetActive(false);
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

            DisabledAllChildElements();
            CalculateReward(numberOfPlayers + 1);
            StartCoroutine(WaitAndShowLoseWindow());
        }
        else if (numberOfPlayers == 1)
        {
            // если остался один игрок, т.е. numberOfPlayers = 1, то кидаем окно победы
            Debug.Log($"Win. Занял {numberOfPlayers} место");

            DisabledAllChildElements();
            CalculateReward(numberOfPlayers);
            StartCoroutine(WaitAndShowWinWindow());
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
    
    private void DisabledAllChildElements()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            GameObject chield = transform.GetChild(i).gameObject;
            if (chield.activeSelf) chield.SetActive(false);
        }
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

    IEnumerator WaitAndShowWinWindow()
    {
        if(playerGO.GetComponent<WheelVehicle>() != null) playerGO.GetComponent<WheelVehicle>().Handbrake = true;

        if (!winWindow.activeSelf) winWindow.SetActive(true);
        if (loseWindow.activeSelf) loseWindow.SetActive(false);

        fragWWText.text = $"{numberOfFrags}";
        goldWWText.text = $"{amountOfGoldReward}";
        cupsWWText.text = $"{amountOfCupReward}";

        yield return new WaitForSeconds(pauseBeforShowingWinWindow);

        gameCamera.gameObject.SetActive(false);
        endGameCamera.gameObject.SetActive(true);
        playerGO.SetActive(false);
    }

    IEnumerator WaitAndShowLoseWindow()
    {
        if (!loseWindow.activeSelf) loseWindow.SetActive(true);
        if (winWindow.activeSelf) winWindow.SetActive(false);

        goldLWText.text = $"{amountOfGoldReward}";

        yield return new WaitForSeconds(pauseBeforShowingLoseWindow);

        gameCamera.gameObject.SetActive(false);
        endGameCamera.gameObject.SetActive(true);
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
