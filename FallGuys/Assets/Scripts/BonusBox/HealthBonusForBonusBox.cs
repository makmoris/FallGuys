using System.Collections;
using UnityEngine;

public class HealthBonusForBonusBox : MonoBehaviour, IBonusForBonusBox
{
    [Min(0)][SerializeField] private int minHealthBonusValue;
    [Min(0)] [SerializeField] private int maxHealthBonusValue;
    [Space]
    [SerializeField] private ParticleSystem healthEffect;

    private HealthBonus healthBonus;

    public Bonus GetBonus()
    {
        healthBonus = new HealthBonus(GetRandomHealthValue());

        return healthBonus;
    }

    public void ShowEffect()
    {
        StartCoroutine(ShowAndDestroy());
    }

    private int GetRandomHealthValue()
    {
        int r = Random.Range(minHealthBonusValue, maxHealthBonusValue + 1);

        return r;
    }

    IEnumerator ShowAndDestroy()
    {
        healthEffect.Play();
        yield return new WaitForSeconds(healthEffect.duration);
        Destroy(gameObject);
    }
}
