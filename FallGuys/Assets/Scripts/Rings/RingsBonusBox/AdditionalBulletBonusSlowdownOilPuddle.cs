using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusSlowdownOilPuddle : AdditionalBulletBonus
{
    [SerializeField]private float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    [SerializeField] private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusSlowdownOilPuddle;

    [Space]
    private float puddleLiveTime;
    [Space]
    [SerializeField] private GameObject slowdownGO;

    private void Awake()
    {
        Type = BonusType.Slowdown;
        puddleLiveTime = time;
        slowdownGO.SetActive(false);
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        transform.localPosition = effectPosition;
        StartCoroutine(PuddleMakingAnimation());
    }

    IEnumerator PuddleMakingAnimation()
    {
        slowdownGO.SetActive(true);
        slowdownGO.transform.localScale = new Vector3(0.125f, 0.125f, 0.125f);
        for (float t = 0; t < 1f; t += Time.deltaTime * 3f)
        {
            slowdownGO.transform.localScale = Vector3.one * t;
            yield return null;
        }
        slowdownGO.transform.localScale = Vector3.one;

        yield return new WaitForSeconds(puddleLiveTime);

        for (float t = 0; t < 1f; t += Time.deltaTime * 3f)
        {
            slowdownGO.transform.localScale = Vector3.one * (1f - t);
            yield return null;
        }

        Destroy(gameObject);
    }
}
