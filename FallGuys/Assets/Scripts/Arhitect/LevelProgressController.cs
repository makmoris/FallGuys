using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class LevelProgressController : MonoBehaviour
{
    protected GameManager _gameManager;
    public GameManager GameManager { set => _gameManager = value; }

    [SerializeField]protected List<GameObject> _playersList = new List<GameObject>();
    [SerializeField]protected List<GameObject> _winnersList = new List<GameObject>();
    [SerializeField] protected List<GameObject> _losersList = new List<GameObject>();

    protected void SendListOfLosersNamesToGameManager()
    {
        List<string> losersNameList = new List<string>();
        foreach(var loser in _losersList)
        {
            PlayerName playerName = loser.GetComponent<PlayerName>();
            if (playerName != null) losersNameList.Add(playerName.Name);
        }

        _gameManager.EliminateLosersFromList(losersNameList);
    }
    
    public abstract void AddPlayer(GameObject playerGO, bool isCurrentPlayer);

    public abstract void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners);
}
