using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressControllerHoneycomb : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private CameraFollowingOnOtherPlayers gameCamCinema;

    [Header("Falling Platform Progress UI Controller")]
    [SerializeField] private ProgressUIControllerHoneycomb progressUIControllerHoneycomb;

    [Header("Falling Platform Settings")]
    [SerializeField] private int numberOfWinners;// сколько должно пройти
    private int numberOfFallens;// сколько должно упасть
    private int currentNumberOfFallens;

    private GameObject currentPlayer;

    [SerializeField]private List<GameObject> playersList = new List<GameObject>();
    [SerializeField]private List<GameObject> losersList = new List<GameObject>();

    public void SetNumberOfWinners(int _numberOfWinners, int _numberOfPlayers)
    {
        numberOfWinners = _numberOfWinners;
        numberOfFallens = _numberOfPlayers - _numberOfWinners;
        progressUIControllerHoneycomb.SetNumberOfFallens(numberOfFallens);
    }

    public void AddPlayer(GameObject playerGO)
    {
        playersList.Add(playerGO);

        gameCamCinema.AddDriver(playerGO);
    }

    public void SetCurrentPlayer(GameObject player)
    {
        currentPlayer = player;
    }

    public void PlayerOut(GameObject playerGO)
    {
        losersList.Add(playerGO);
        playersList.Remove(playerGO);

        currentNumberOfFallens++;

        if(currentNumberOfFallens == numberOfFallens)
        {
            // конец игры. ѕоказываем пост боевой экран
            Debug.Log("END");
        }

        // update ui
        progressUIControllerHoneycomb.UpdateNamberOfFallens(currentNumberOfFallens);
    }
}
