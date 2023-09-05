using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelProgressController : MonoBehaviour
{
    [Header("Progress UI Controller")]
    [SerializeField] protected LevelProgressUIController levelProgressUIController;
    [Header("Post Level Place Controller")]
    [SerializeField] protected PostLevelPlaceController postLevelPlaceController;
    [Header("Camera Following On Other Players")]
    [SerializeField] protected CameraFollowingOnOtherPlayers cameraFollowingOnOtherPlayers;

    protected GameManager _gameManager;
    public GameManager GameManager { set => _gameManager = value; }

    protected List<GameObject> _playersList = new List<GameObject>();
    protected List<GameObject> _winnersList = new List<GameObject>();
    protected List<GameObject> _losersList = new List<GameObject>();

    protected bool _isCurrentPlayerWinner;
    protected bool _isGameEnded;
    
    protected virtual void OnEnable()
    {
        levelProgressUIController.GameCanStartEvent += StartGame;
    }

    protected void SendListOfLosersNamesToGameManager()
    {
        List<string> losersNameList = new List<string>();
        foreach(var loser in _losersList)
        {
            PlayerName playerName = loser.GetComponent<PlayerName>();
            if (playerName != null) losersNameList.Add(playerName.Name);
        }

        //_losersList.Clear();

        _gameManager.EliminateLosersFromList(losersNameList);
    }
    
    public abstract void AddPlayer(GameObject playerGO, bool isCurrentPlayer);

    public abstract void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners);

    protected abstract void ShowPostWindow();

    protected abstract void StartGame();

    protected virtual void OnDisable()
    {
        levelProgressUIController.GameCanStartEvent -= StartGame;
    }
}
