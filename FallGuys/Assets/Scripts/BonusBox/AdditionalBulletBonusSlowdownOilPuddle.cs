using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdditionalBulletBonusSlowdownOilPuddle : AdditionalBulletBonus
{
    [Header("Ignore Parent")]
    public bool isIgnoreParent;

    [Header("Slowdown Time After Exit")]
    [Min(0)]public float slowdownTimeAfterExitForAI = 2f;
    [Min(0)] public float slowdownTimeAfterExitForPlayer = 1f;

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
        SetPositionOnGround(effectPosition);

        StartCoroutine(PuddleMakingAnimation(parentGO));
    }

    public override Bonus GetBonus()
    {
        slowdownBonus = new SlowdownBonus(decelerationAmount, puddleLiveTime);
        return slowdownBonus;
    }

    private void SetPositionOnGround(Vector3 effectPosition)
    {
        int layerMask = 1 << 3;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 100f, layerMask))
        {
            transform.localPosition = new Vector3(effectPosition.x, effectPosition.y - hit.distance, effectPosition.z);

            if(hit.transform.parent != null)
            {
                Quaternion worldRotation = hit.transform.parent.rotation * hit.transform.localRotation;
                transform.localRotation = worldRotation;
            }
            else
            {
                transform.localRotation = hit.transform.localRotation;
            }
        }
        else
        {
            transform.localPosition = effectPosition;
        }
    }

    IEnumerator PuddleMakingAnimation(GameObject parentGO)
    {
        slowdownGO.SetActive(true);

        if (isIgnoreParent)
        {
            slowdownObstacle.SetIgnoreParent(parentGO);
        }

        if(slowdownTimeAfterExitForAI > 0 && slowdownTimeAfterExitForPlayer > 0)
        {
            slowdownObstacle.SetSlowTimeAfterExit(slowdownTimeAfterExitForAI, slowdownTimeAfterExitForPlayer);
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
