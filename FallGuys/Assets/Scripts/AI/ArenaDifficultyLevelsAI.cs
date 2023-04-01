using UnityEngine;

public class ArenaDifficultyLevelsAI : MonoBehaviour
{
    [SerializeField] private ArenaDifficultyLevelData arenaDifficultyLevelData;

    private float shotDecisionSpeed;
    private float frequencyOfTargetingPlayer;
    private DuelType duelType;

    private bool dataWasLoad;

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        shotDecisionSpeed = arenaDifficultyLevelData.ShotDecisionSpeed;
        frequencyOfTargetingPlayer = arenaDifficultyLevelData.FrequencyOfTargetingPlayer;
        duelType = arenaDifficultyLevelData.GetDuelType();
        dataWasLoad = true;
    }

    public float GetShotDecisionSpeed()
    {
        if (!dataWasLoad) LoadData();

        return shotDecisionSpeed;
    }

    public float GetFrequencyOfTargetingPlayer()
    {
        if (!dataWasLoad) LoadData();

        return frequencyOfTargetingPlayer;
    }

    public DuelType GetDuelType()
    {
        return duelType;
    }
}
