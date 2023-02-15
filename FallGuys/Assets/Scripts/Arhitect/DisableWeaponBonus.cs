using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableWeaponBonus : Bonus
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
