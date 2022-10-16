using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private Bullet bulletPrefab;
    [Space]

    public float damage;
    public float rechargeTime;// через корутину. Ѕульку, если закончилась перезар€дка - оп€ть можно стрел€ть
    public float attackRange;// дальность - это скеил по x и z. ѕо y всегда должен быть большой, чтобы пробивал все

    public Collider parentBodyCollider;
    public Vector3 bulletPosition;
    public Transform weaponTransform;
    [SerializeField] private Transform target;

    [SerializeField] private bool canAttack;


    [SerializeField] private Transform startBulletPosition;

    private PoolMono<Bullet> bulletPool;
    private bool nextShot = true;

    private void Start()
    {
        bulletPool = new PoolMono<Bullet>(bulletPrefab, 5);
        bulletPool.autoExpand = autoExpand;
    }

    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("Fire1") && nextShot)
            {
                // спавним пулю из дула пушки. /// ќна уже есть(без спавна). ¬ыстреливаем ею 
                StartCoroutine(Shot());
                nextShot = false;
                //bullet.GetComponent<Bullet>().Shot();
                // перезар€жаем корутинка дл€ notReacharge
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

    IEnumerator Shot()
    {
        var bullet = bulletPool.GetFreeElement();

        // сделать присваивание коллайдера родител€ и характеристик в одном месте, один раз. „тобы не делать это каждый раз
        bullet.SetParent(parentBodyCollider);
        bullet.SetDamageValue(damage);

        bullet.transform.position = startBulletPosition.position;
        bullet.transform.rotation = startBulletPosition.rotation;
        yield return new WaitForSeconds(rechargeTime);
        nextShot = true;
    }
}
