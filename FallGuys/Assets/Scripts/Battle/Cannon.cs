using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private Bullet bulletPrefab;

    private PoolMono<Bullet> bulletPool;
    private Bullet bulletExample;

    [Header("Characteristics")]
    public float damage;
    public float rechargeTime;
    public float attackRange;
    
    [Header("Weapon parts")]
    [SerializeField] private Collider parentBodyCollider;
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform detectorTransform;
    [SerializeField] private Transform startBulletPosition;

    private Transform target;// понадобитс€, если делать полет пули не по премой, а в цель (автонаведение)


    private bool canAttack;
    private bool nextShot = true;


    private void Start()
    {
        bulletPool = new PoolMono<Bullet>(bulletPrefab, 5);
        bulletPool.autoExpand = autoExpand;

        SetDetectorScale();

        bulletExample = Instantiate(bulletPrefab, startBulletPosition.position, Quaternion.identity, startBulletPosition);
        bulletExample.GetComponent<Bullet>().enabled = false;
    }

    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("CarAttack") && nextShot)
            {
                // спавним пулю из дула пушки. /// ќна уже есть(без спавна). ¬ыстреливаем ею 
                StartCoroutine(Shot());
                nextShot = false;
            }
        }
    }

    // два метода вызываютс€ из TargetDetector при обнаружении цели
    public void SetObjectForAttack(Transform objForAttackTransform)
    {
        weaponTransform.LookAt(objForAttackTransform);
        target = objForAttackTransform;
        canAttack = true;
    }

    public void ReturnWeaponToPosition()
    {
        weaponTransform.localRotation = Quaternion.Euler(0f, 0f, 0f);
        canAttack = false;
    }

    private void SetDetectorScale()
    {
        detectorTransform.localScale = Vector3.one * attackRange;
        detectorTransform.localPosition = new Vector3(0f, 0f, detectorTransform.localScale.y / 2f);
    }

    private void ShowBulletExample()
    {
        bulletExample.gameObject.SetActive(true);
    }
    private void HideBulletExample()
    {
        bulletExample.gameObject.SetActive(false);
    }

    IEnumerator Shot()
    {
        HideBulletExample();

        var bullet = bulletPool.GetFreeElement();

        bullet.SetParent(parentBodyCollider);
        bullet.SetDamageValue(damage);

        bullet.transform.position = startBulletPosition.position;
        bullet.transform.rotation = startBulletPosition.rotation;
        yield return new WaitForSeconds(rechargeTime);
        nextShot = true;
        ShowBulletExample();
    }
}
