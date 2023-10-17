using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BonusBox : MonoBehaviour
{
    [System.Flags]
    public enum Bonuses
    {
        Nothing = 0,
        Health = 1,
        Shield = 2,
        Lightning = 4,
        SlowdownOilPuddle = 8,
        Explosion = 16,
        ControlInversion = 32,
        BlankShot = 64
    }

    [SerializeField] private Bonuses bonusesFlags;
    [SerializeField] private List<Bonuses> activeBonusesFlagsList = new List<Bonuses>();

    [Header("Bonus Prefabs")]
    [SerializeField] private HealthBonusForBonusBox healthBonusForBonusBox;
    [SerializeField] private ShieldBonusForBonusBox shieldBonusForBonusBox;
    [SerializeField] private AdditionalBulletBonusLightning additionalBulletBonusLightning;
    [SerializeField] private AdditionalBulletBonusSlowdownOilPuddle additionalBulletBonusSlowdownOilPuddle;
    [SerializeField] private AdditionalBulletBonusExplosion additionalBulletBonusExplosion;
    [SerializeField] private AdditionalBulletBonusControlInversion additionalBulletBonusControlInversion;
    [SerializeField] private AdditionalBulletBonusBlankShot additionalBulletBonusBlankShot;

    [Header("Bonus Box")]
    [Min(0)][SerializeField] private float bonusBoxRespawnTime = 4f;
    [SerializeField] private GameObject bonusBoxGO;

    private void Awake()
    {
        FillActiveBonusesList();
    }


    public void ApplyBonus(GameObject car)
    {
        Bonuses randomBonusFlag = activeBonusesFlagsList[Random.Range(0, activeBonusesFlagsList.Count)];

        Bumper bumper = car.GetComponent<Bumper>();
        Weapon weapon = car.GetComponentInChildren<Weapon>();

        switch (randomBonusFlag)
        {
            case Bonuses.Health:

                bumper.GetBonus(healthBonusForBonusBox.GetBonus());

                HealthBonusForBonusBox healthBonus = Instantiate(healthBonusForBonusBox, transform.position, Quaternion.identity);
                healthBonus.ShowEffect();

                break;

            case Bonuses.Shield:

                bumper.GetBonus(shieldBonusForBonusBox.GetBonus());

                break;

            case Bonuses.Lightning:

                weapon.SetAdditionalBulletBonus(additionalBulletBonusLightning, true);

                break;

            case Bonuses.SlowdownOilPuddle:

                weapon.SetAdditionalBulletBonus(additionalBulletBonusSlowdownOilPuddle, true);

                break;

            case Bonuses.Explosion:

                weapon.SetAdditionalBulletBonus(additionalBulletBonusExplosion, true);

                break;

            case Bonuses.ControlInversion:

                weapon.SetAdditionalBulletBonus(additionalBulletBonusControlInversion, true);

                break;

            case Bonuses.BlankShot:

                weapon.SetAdditionalBulletBonus(additionalBulletBonusBlankShot, true);

                break;
        }

        CoroutineRunner.Run(WaitAndRespawn());
    }

    private void FillActiveBonusesList()
    {
        if (bonusesFlags.HasFlag(Bonuses.Health)) activeBonusesFlagsList.Add(Bonuses.Health);
        if (bonusesFlags.HasFlag(Bonuses.Shield)) activeBonusesFlagsList.Add(Bonuses.Shield);
        if (bonusesFlags.HasFlag(Bonuses.Lightning)) activeBonusesFlagsList.Add(Bonuses.Lightning);
        if (bonusesFlags.HasFlag(Bonuses.SlowdownOilPuddle)) activeBonusesFlagsList.Add(Bonuses.SlowdownOilPuddle);
        if (bonusesFlags.HasFlag(Bonuses.Explosion)) activeBonusesFlagsList.Add(Bonuses.Explosion);
        if (bonusesFlags.HasFlag(Bonuses.ControlInversion)) activeBonusesFlagsList.Add(Bonuses.ControlInversion);
        if (bonusesFlags.HasFlag(Bonuses.BlankShot)) activeBonusesFlagsList.Add(Bonuses.BlankShot);
    }

    IEnumerator WaitAndRespawn()
    {
        bonusBoxGO.SetActive(false);
        yield return new WaitForSeconds(bonusBoxRespawnTime);
        bonusBoxGO.SetActive(true);
    }
}
