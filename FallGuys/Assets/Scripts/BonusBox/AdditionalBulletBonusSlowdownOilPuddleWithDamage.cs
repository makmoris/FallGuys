using Sirenix.OdinInspector;
using UnityEngine;

public class AdditionalBulletBonusSlowdownOilPuddleWithDamage : AdditionalBulletBonusSlowdownOilPuddle
{
    [Header("Damage")]
    [MaxValue(0)] [SerializeField] private float damage = -5f;
    [SerializeField] private float damageInterval = 3f;

    public override void UseDamage()
    {
        slowdownObstacle.SetDamage(damage, damageInterval);
    }
}
