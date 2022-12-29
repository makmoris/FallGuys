using UnityEngine;

public class DifficultyLevelsAI : MonoBehaviour
{
    [SerializeField] private DifficultyLevelData difficultyLevelData;

    private float shotDecisionSpeed;
    private float frequencyOfTargetingPlayer;

    private bool dataWasLoad;

    private void Awake()
    {
        LoadData();
    }

    private void LoadData()
    {
        shotDecisionSpeed = difficultyLevelData.ShotDecisionSpeed;
        frequencyOfTargetingPlayer = difficultyLevelData.FrequencyOfTargetingPlayer;
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
}
