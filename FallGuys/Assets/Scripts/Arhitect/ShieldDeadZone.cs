using UnityEngine;

public class ShieldDeadZone : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    public override void Got()
    {
        
    }
}
