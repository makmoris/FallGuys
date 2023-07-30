using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressControllerHoneycomb : LevelProgressController
{
    [Header("Camera")]
    [SerializeField] private CameraFollowingOnOtherPlayers gameCamCinema;

    [Header("Falling Platform Progress UI Controller")]
    [SerializeField] private ProgressUIControllerHoneycomb progressUIControllerHoneycomb;

    [Header("Falling Platform Settings")]
    private int numberOfWinners;
    private int numberOfFallens;// сколько должно упасть
    private int currentNumberOfFallens;

    private GameObject currentPlayer;

    [SerializeField]private List<GameObject> playersList = new List<GameObject>();


    public override void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners)
    {
        numberOfWinners = _numberOfWinners;
        numberOfFallens = _numberOfPlayers - _numberOfWinners;
        progressUIControllerHoneycomb.SetNumberOfFallens(numberOfFallens);
    }

    public override void AddPlayer(GameObject playerGO)
    {
        playersList.Add(playerGO);

        gameCamCinema.AddDriver(playerGO);
    }

    public override void SetCurrentPlayer(GameObject currentPlayerGO)
    {
        currentPlayer = currentPlayerGO;
    }

    public void PlayerOut(GameObject playerGO)
    {
        _losersList.Add(playerGO);
        playersList.Remove(playerGO);

        currentNumberOfFallens++;

        if(currentNumberOfFallens == numberOfFallens)
        {
            // конец игры. Показываем пост боевой экран
            Debug.Log("END");
        }

        // update ui
        progressUIControllerHoneycomb.UpdateNamberOfFallens(currentNumberOfFallens);
    }
}
