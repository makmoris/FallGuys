using UnityEngine;

public enum BonusType
{
    AddHealth = 0,
    AddShield = 1,
    AddGold = 2,
    DisableWeapon = 3,
    DisableWeaponFromLightning = 4,
    Slowdown = 5
    //AddSpeed = 2,
    //AddDamage = 3
}
public abstract class Bonus : MonoBehaviour
{
    public BonusType Type;
    //public float value;
    public abstract float Value { get; set; }

    public abstract float BonusTime { get; set; }

    public virtual void Got()
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);
    }

    public virtual void Test()
    {
        Debug.Log("");
    }
}
