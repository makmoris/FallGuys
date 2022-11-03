using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 5;
    [SerializeField] private bool autoExpand = true;
    [SerializeField] private Bullet bulletPrefab;

    private PoolMono<Bullet> bulletPool;
    private Bullet bulletExample;

    [Header("Characteristics")]
    [SerializeField] private WeaponCharacteristicsData characteristicsData;
    [SerializeField] private float damage;
    [SerializeField] private float rechargeTime;
    [SerializeField] private float attackRange;

    [Header("Weapon parts")]
    [SerializeField] private Collider parentBodyCollider;// у каждой машинки свой // убрать —ерриализацию
    [SerializeField] private GameObject parentShield;// щит этой машинки, чтобы игнорировать его при выстреле. „тобы пул€ пролетала через него // убрать
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform detectorTransform;
    [SerializeField] private Transform startBulletPosition;

    [Header("AI")]
    [SerializeField] private bool isAI;


    private Transform target;// понадобитс€, если делать полет пули не по пр€мой, а в цель (автонаведение)


    private Quaternion defaultWeaponRotation;
    private bool canAttack = true;
    private bool canAIAttack;
    private bool nextShot = true;


    private void Awake()
    {
        damage = characteristicsData.damage;
        rechargeTime = characteristicsData.rechargeTime;
        attackRange = characteristicsData.attackRange;

        detectorTransform.GetComponent<AttackTargetDetector>().IsAI(isAI);
    }

    private void Start()
    {
        bulletPool = new PoolMono<Bullet>(bulletPrefab, poolCount);
        bulletPool.autoExpand = autoExpand;

        SetDetectorScale();

        bulletExample = Instantiate(bulletPrefab, startBulletPosition.position, Quaternion.identity, startBulletPosition);
        bulletExample.GetComponent<Bullet>().enabled = false;
        bulletExample.GetComponent<Collider>().enabled = false;
        bulletExample.transform.Find("Jet02Red").gameObject.SetActive(false);

        defaultWeaponRotation = weaponTransform.localRotation;
    }

    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("CarAttack") && nextShot && !isAI)
            {
                // спавним пулю из дула пушки. /// ќна уже есть(без спавна). ¬ыстреливаем ею 
                StartCoroutine(Shot());
                nextShot = false;
            }
        }

        if (isAI && canAIAttack && nextShot)
        {
            StartCoroutine(Shot());
            nextShot = false;
        }
    }

    // два метода вызываютс€ из TargetDetector при обнаружении цели
    public void SetObjectForAttack(Transform objForAttackTransform)
    {
        weaponTransform.LookAt(objForAttackTransform);
        target = objForAttackTransform;
        //canAttack = true;
        canAIAttack = true;
    }

    public void ReturnWeaponToPosition()
    {
        weaponTransform.localRotation = defaultWeaponRotation;
        //canAttack = false;
        canAIAttack = false;
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

    public void SetParentBodyCollider(Collider bodyCollider)
    {
        parentBodyCollider = bodyCollider;
        parentShield = parentBodyCollider.transform.GetComponentInChildren<Shield>(true).gameObject;
    }

    public void IsAI(bool value)// вызываетс€ в installer при создании бота
    {
        isAI = value;
        detectorTransform.GetComponent<AttackTargetDetector>().IsAI(isAI);
    }

    IEnumerator Shot()
    {
        HideBulletExample();

        var bullet = bulletPool.GetFreeElement();

        bullet.SetParent(parentBodyCollider, parentShield);
        bullet.SetDamageValue(damage);

        bullet.transform.position = startBulletPosition.position;
        bullet.transform.rotation = startBulletPosition.rotation;
        yield return new WaitForSeconds(rechargeTime);
        nextShot = true;
        ShowBulletExample();
    }

    private void OnDisable()
    {
        nextShot = true;
        ShowBulletExample();
    }
}
