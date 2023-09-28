using UnityEngine;

public abstract class Reward : MonoBehaviour
{
    public RewardType RewardType;

    [SerializeField] [Min(1)] protected int rewardValue;
    public int RewardValue { get => rewardValue; private set => rewardValue = value; }
}
