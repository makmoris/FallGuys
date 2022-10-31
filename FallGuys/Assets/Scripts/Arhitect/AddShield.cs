using UnityEngine;

public class AddShield : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    public override void Got()
    {
        // заглушка, чтобы не удалять объект
    }
}
