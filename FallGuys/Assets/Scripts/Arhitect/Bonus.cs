using UnityEngine;

public enum BonusType
{
    AddHealth = 0,
    AddSpeed = 1,
    AddDamage = 2
}
public abstract class Bonus : MonoBehaviour
{
    public BonusType Type;
    public float Value;

    public void Got()
    {
        Destroy(gameObject);
    }
}
