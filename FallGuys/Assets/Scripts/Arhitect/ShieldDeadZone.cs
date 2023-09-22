using UnityEngine;

public class ShieldDeadZone : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }

    public override void Got()
    {
        
    }
}
