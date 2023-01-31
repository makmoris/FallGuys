using UnityEngine;

public enum FrequencyType
{
    standard = 0,
    low = 1,
    medium = 2,
    hard = 3
}

public enum DuelType
{
    randomFromPlayerAndBonus = 0, // выбор цели на рандоме между игроком и всеми бонус боксами
    alternationBetweenPlayerAndBonusBonus = 1,
    alternationBetweenPlayerAndBonus = 2,
    playerOnly = 3
}

[CreateAssetMenu(fileName = "New Difficulty Level")]
public class DifficultyLevelData : ScriptableObject
{
    [Header("Время принятия решения атаковать цель")]
    [SerializeField] private float shotDecisionSpeed;
    [Space]

    [Space]
    [Header("Описание вариантов частоты выбора целью игрока:")]
    [Header("1) Игрок имеет одинаковую вероятность выбора с остальными целями")]
    [Header("2) 25% целей - игрок")]
    [Header("3) 50% целей - игрок")]
    [Header("4) 75% целей - игрок")]
    [Space]
    [SerializeField] private FrequencyType frequencyOfTargetingPlayer;
    [Space]

    [Space]
    [Header("Описание вариантов дуэли:")]
    [Header("1) Цель выбирается рандомно из игрока и бонусных боксов")]
    [Header("2) Цель чередуется. Игрок-Бонус-Бонус...")]
    [Header("3) Цель чередуется. Игрок-Бонус...")]
    [Header("4) Цель - только игрок")]
    [Space]
    [SerializeField] private DuelType duelTargetsVariant;

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

    public DuelType GetDuelType()
    {
        return duelTargetsVariant;
    }
}
