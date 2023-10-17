using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusSlowdownOilPuddle : AdditionalBulletBonus
{
    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusSlowdownOilPuddle;

    private SlowdownBonus slowdownBonus;
    [SerializeField] private float decelerationAmount = 1.5f;

    [Space]
    [SerializeField] private GameObject slowdownGO;
    [SerializeField] private float puddleLiveTime = 5f;

    private void Awake()
    {
        slowdownGO.SetActive(false);
    }

    public override void PlayEffect(Vector3 effectPosition)
    {
        transform.localPosition = effectPosition;
        StartCoroutine(PuddleMakingAnimation());
    }

    public override Bonus GetBonus()
    {
        slowdownBonus = new SlowdownBonus(decelerationAmount, puddleLiveTime);
        return slowdownBonus;
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
