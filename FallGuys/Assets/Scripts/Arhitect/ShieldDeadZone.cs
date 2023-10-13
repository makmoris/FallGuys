using UnityEngine;

public class ShieldDeadZone : Bonus
{
     float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [SerializeField] private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }
}
