using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusSlowdownOilPuddle : AdditionalBulletBonus
{
    [Header("Ignore Parent")]
    public bool isIgnoreParent;

    [Header("Slowdown Time After Exit")]
    [Min(0)]public float slowdownTimeAfterExit;

    public override AdditionalBulletBonusTypeEnum AdditionalBulletBonusType => AdditionalBulletBonusTypeEnum.AdditionalBulletBonusSlowdownOilPuddle;

    private SlowdownBonus slowdownBonus;
    [Space]
    [SerializeField] private float decelerationAmount = 1.5f;

    [Space]
    [SerializeField] private GameObject slowdownGO;
    [SerializeField] private float puddleLiveTime = 5f;

    private SlowdownObstacle slowdownObstacle;

    private void Awake()
    {
        slowdownGO.SetActive(false);
        slowdownObstacle = GetComponentInChildren<SlowdownObstacle>(true);
    }

    public override void PlayEffect(Vector3 effectPosition, GameObject parentGO)
    {
        transform.localPosition = effectPosition;
        StartCoroutine(PuddleMakingAnimation(parentGO));
    }

    public override Bonus GetBonus()
    {
        slowdownBonus = new SlowdownBonus(decelerationAmount, puddleLiveTime);
        return slowdownBonus;
    }

    IEnumerator PuddleMakingAnimation(GameObject parentGO)
    {
        slowdownGO.SetActive(true);

        if (isIgnoreParent)
        {
            slowdownObstacle.SetIgnoreParent(parentGO);
        }

        if(slowdownTimeAfterExit > 0)
        {
            slowdownObstacle.SetSlowTimeAfterExit(slowdownTimeAfterExit);
        }

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
