using UnityEngine;

public enum FrequencyType
{
    standard = 0,
    low = 1,
    medium = 2,
    hard = 3
}

[CreateAssetMenu(fileName = "New Difficulty Level")]
public class DifficultyLevelData : ScriptableObject
{
    [SerializeField] private float shotDecisionSpeed;
    [SerializeField] private FrequencyType frequencyOfTargetingPlayer;

    public float ShotDecisionSpeed
    {
        get
        {
            return shotDecisionSpeed;
        }
    }

    public float FrequencyOfTargetingPlayer
    {
        get     // сколько процентов от N целей добавить игроков, чтобы чаще выпадал выбор на игрока
        {
            float percent = 0;

            switch (frequencyOfTargetingPlayer)
            {
                case FrequencyType.standard:
                    percent = 0;
                    break;
                case FrequencyType.low:
                    percent = 0.25f;
                    break;
                case FrequencyType.medium:
                    percent = 0.5f;
                    break;
                case FrequencyType.hard:
                    percent = 0.75f;
                    break;
            }

            return percent;
        }
    }
}
