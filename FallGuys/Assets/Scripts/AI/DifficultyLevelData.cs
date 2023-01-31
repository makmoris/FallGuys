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
    randomFromPlayerAndBonus = 0, // ����� ���� �� ������� ����� ������� � ����� ����� �������
    alternationBetweenPlayerAndBonusBonus = 1,
    alternationBetweenPlayerAndBonus = 2,
    playerOnly = 3
}

[CreateAssetMenu(fileName = "New Difficulty Level")]
public class DifficultyLevelData : ScriptableObject
{
    [Header("����� �������� ������� ��������� ����")]
    [SerializeField] private float shotDecisionSpeed;
    [Space]

    [Space]
    [Header("�������� ��������� ������� ������ ����� ������:")]
    [Header("1) ����� ����� ���������� ����������� ������ � ���������� ������")]
    [Header("2) 25% ����� - �����")]
    [Header("3) 50% ����� - �����")]
    [Header("4) 75% ����� - �����")]
    [Space]
    [SerializeField] private FrequencyType frequencyOfTargetingPlayer;
    [Space]

    [Space]
    [Header("�������� ��������� �����:")]
    [Header("1) ���� ���������� �������� �� ������ � �������� ������")]
    [Header("2) ���� ����������. �����-�����-�����...")]
    [Header("3) ���� ����������. �����-�����...")]
    [Header("4) ���� - ������ �����")]
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
        get     // ������� ��������� �� N ����� �������� �������, ����� ���� ������� ����� �� ������
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
