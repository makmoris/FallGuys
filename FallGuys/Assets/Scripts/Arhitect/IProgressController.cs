using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IProgressController
{
    public void SetNumberOfPlayersAndWinners(int _numberOfPlayers, int _numberOfWinners);
    public void AddPlayer(GameObject playerGO);
    public void SetCurrentPlayer(GameObject currentPlayerGO);
}
