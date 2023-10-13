using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    [System.Flags]
    public enum VehicleBonuses
    {
        Nothing = 0,
        Health = 1,
        Shield = 2
    }

    [System.Flags]
    public enum BulletBonuses
    {
        Nothing = 0,
        Lightning = 1,
        SlowdownOilPuddle = 2,
        Explosion = 4,
        ControlInversion = 8,
        BlankShot = 16
    }

    [SerializeField] private VehicleBonuses vehicleBonusesFlags;
    [SerializeField] private BulletBonuses bulletBonusesFlags;

    private List<Bonus> usingBonuses;

    [Header("Vehicle Bonuses Settings")]
    [Header("Health")]
    [Min(1)] [SerializeField] private int minHealthBonusValue = 1;
    [Min(1)] [SerializeField] private int maxHealthBonusValue = 10;
    [Space]
    [SerializeField] private ParticleSystem healthEffect;

    [Header("Shield")]
    [Min(1)] [SerializeField] private int minShieldBonusTime = 1;
    [Min(1)] [SerializeField] private int maxShieldBonusTime = 5;

    [Header("Bullet Bonuses Settings")]
    [SerializeField] private List<AdditionalBulletBonus> additionalBulletBonusesPrefabList;

    private BonusBoxTrigger bonusBoxTrigger;

    private void Awake()
    {
        bonusBoxTrigger = GetComponentInChildren<BonusBoxTrigger>();
        bonusBoxTrigger.Initialize(this);
    }

    public void ApplyBonus(GameObject carGO)
    {
        int r = Random.Range(0, 2);

        if (r == 0) ApplyVehicleBonus(carGO);
        else ApplyBulletBonus(carGO);
    }

    private void ApplyVehicleBonus(GameObject carGO)
    {
        Bumper bumper = carGO.GetComponent<Bumper>();

        //switch (vehicleBonuses)
        //{
        //    case VehicleBonuses.Health:

        //        Type = BonusType.AddHealth;
        //        Value = Random.Range(minHealthBonusValue, maxHealthBonusValue + 1);

        //        break;
        //    case VehicleBonuses.Shield:

        //        Type = BonusType.AddShield;
        //        BonusTime = Random.Range(minShieldBonusTime, maxShieldBonusTime + 1);

        //        break;
        //}
    }

    private void ApplyBulletBonus(GameObject carGO)
    {
        Weapon weapon = carGO.GetComponentInChildren<Weapon>();

        
    }

    private AdditionalBulletBonus GetRandomAdditionalBonus()
    {
        int rand = Random.Range(0, additionalBulletBonusesPrefabList.Count);

        return additionalBulletBonusesPrefabList[rand];
    }

    private void CheckBonusesFlags()
    {
        if (vehicleBonusesFlags.HasFlag(VehicleBonuses.Health))
        {

            //usingBonuses.Add();
        }
    }
    
}
