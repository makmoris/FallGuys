using System;
using System.Collections;
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

    private int enumLength;

    [Space]
    [SerializeField] private float boxRespawnTime;

    [Header("Health")]
    [SerializeField] int minHealthBonusValue;
    [SerializeField] int maxHealthBonusValue;

    [Header("Shield")]
    [SerializeField] int minShieldBonusValue;
    [SerializeField] int maxShieldBonusValue;

    [Header("Gold")]
    [SerializeField] int minGoldBonusValue;
    [SerializeField] int maxGoldBonusValue;

    private void Awake()
    {
        enumLength = Enum.GetNames(typeof(BonusType)).Length;
    }

    private void OnEnable()
    {
        Type = GetRandomBonus();
        value = GetRandomValue(Type);
    }

    private BonusType GetRandomBonus()
    {
        int rand = Random.Range(0, enumLength);
        BonusType bonusType = (BonusType)Enum.GetValues(typeof(BonusType)).GetValue(rand);

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
        CoroutineRunner.Run(WaitAndRespawn());
    }

    IEnumerator WaitAndRespawn()
    {
        gameObject.SetActive(false);
        yield return new WaitForSeconds(boxRespawnTime);
        gameObject.SetActive(true);
    }
}
