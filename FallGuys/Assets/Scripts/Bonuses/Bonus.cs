using UnityEngine;

public enum BonusType
{
    AddHealth = 0,
    AddShield = 1,
    AddGold = 2,
    DisableWeapon = 3,
    DisableWeaponFromLightning = 4,
    Slowdown = 5,
    ControlInversion = 6
}
public abstract class Bonus : MonoBehaviour
{
    public BonusType Type;
    public abstract float Value { get; set; }

    public abstract float BonusTime { get; set; }

    //public virtual void Got()
    //{
    //    //Destroy(gameObject);
    //    gameObject.SetActive(false);
    //}
}
