using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaPostPlaceController : PostLevelPlaceController
{
    [Header("Arena Post Place UI Controller")]
    [SerializeField] private ArenaPostPlaceUIController arenaPostPlaceUIController;

    [Space]
    [SerializeField] private EndGameController endGameController;

    public void EnabledAudioListener()
    {
        endGameController.EnabledAudioListener();
    }

    public void PlayerWin(int numberOfFrags, int amountOfGoldReward, int amountOfCupReward)
    {
        arenaPostPlaceUIController.ShowPlayerWinWindow(numberOfFrags, amountOfGoldReward, amountOfCupReward);
    }

    public void PlayerLose(int numberOfFrags, int amountOfGoldReward, int amountOfCupReward)
    {
        arenaPostPlaceUIController.ShowPlayerLoseWindow(numberOfFrags, amountOfGoldReward, amountOfCupReward);
    }
}
