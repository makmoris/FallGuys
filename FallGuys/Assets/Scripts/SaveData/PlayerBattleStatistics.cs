using System;

namespace PunchCars.SaveData
{
    [Serializable]
    public class PlayerBattleStatistics
    {
        public int battlesAmount;
        public int numberOfFirstPlaces;// количество первых мест занятых игроком
    }
}