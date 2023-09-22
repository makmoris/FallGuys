using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AddBonus : Bonus
{
    [SerializeField] private float value;
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

    private int enumLength;

    [Space]
    [SerializeField] private float boxRespawnTime;

    [Header("Health")]
    [SerializeField] private int minHealthBonusValue;
    [SerializeField] private int maxHealthBonusValue;
    [Space]
    [SerializeField] private ParticleSystem healthEffect;

    [Header("Shield")]
    [SerializeField] private int minShieldBonusValue;
    [SerializeField] private int maxShieldBonusValue;

    [Header("Gold")]
    [SerializeField] private int minGoldBonusValue;
    [SerializeField] private int maxGoldBonusValue;
    [Space]
    [SerializeField] private ParticleSystem goldEffect;
    [SerializeField] private AudioClip goldSound;

    List<BonusType> bonuses = new List<BonusType>();

    [Space]
    [SerializeField] private AudioSource audioSource;

    private void Awake()
    {
        enumLength = Enum.GetNames(typeof(BonusType)).Length;

        FillBonusesList();
    }

    private void FillBonusesList()
    {
        for (int i = 0; i < enumLength; i++)
        {
            BonusType bonusType = (BonusType)Enum.GetValues(typeof(BonusType)).GetValue(i);

            if (bonusType != BonusType.DisableWeaponFromLightning && bonusType != BonusType.DisableWeapon)
            {
                bonuses.Add(bonusType);
            }
        }
    }

    private void OnEnable()
    {
        Type = GetRandomBonus();
        value = GetRandomValue(Type);
    }

    private BonusType GetRandomBonus()
    {
        int rand = Random.Range(0, bonuses.Count);

        BonusType bonusType = bonuses[rand];

        return bonusType;
    }

    private int GetRandomValue(BonusType bonusType)
    {
        int value = 0;

        switch (bonusType)
        {
            case BonusType.AddHealth:

                int randH = Random.Range(minHealthBonusValue, maxHealthBonusValue + 1);
                value = randH;

                break;

            case BonusType.AddShield:

                int randS = Random.Range(minShieldBonusValue, maxShieldBonusValue + 1);
                value = randS;

                break;

            case BonusType.AddGold:

                int randG = Random.Range(minGoldBonusValue, maxGoldBonusValue + 1);
                value = randG;

                break;
        }

        return value;
    }

    public override void Got()
    {
        if (Type == BonusType.AddHealth)
        {
            healthEffect.Play();
        }
        if (Type == BonusType.AddGold)
        {
            goldEffect.Play();

            audioSource.clip = goldSound;
            audioSource.Play();
        }

        CoroutineRunner.Run(WaitAndRespawn());
    }

    IEnumerator WaitAndRespawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(boxRespawnTime);
        gameObject.SetActive(true);
    }
}
