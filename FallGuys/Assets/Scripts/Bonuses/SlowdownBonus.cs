public class SlowdownBonus : Bonus
{
    private float slowdownTime;
    public float SlowdownTime { get => slowdownTime; }

    public SlowdownBonus(float decelerationValue, float timeValue)
    {
        _bonusType = BonusType.Slowdown;
        _bonusValue = decelerationValue;
        slowdownTime = timeValue;
    }
}
