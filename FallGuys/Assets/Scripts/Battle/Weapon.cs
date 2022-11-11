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
    private float damage;
    private float rechargeTime;
    private float attackRange;

    [Header("Weapon parts")]
    [SerializeField] private Transform weaponTransform;
    [SerializeField] private Transform detectorTransform;
    [SerializeField] private Transform startBulletPosition;
    private Collider parentBodyCollider;// у каждой машинки свой // убрать Серриализацию
    private GameObject parentShield;// щит этой машинки, чтобы игнорировать его при выстреле. Чтобы пуля пролетала через него // убрать

    //[Header("AI")]
    private bool isAI;

    //[Header("Is lobby scene?")]
    //[SerializeField] private bool isLobby;

    private Transform target;// понадобится, если делать полет пули не по прямой, а в цель (автонаведение)


    private Quaternion defaultWeaponRotation;
    private bool canAttack = true;
    private bool canAIAttack;
    private bool nextShot = true;


    private void Awake()
    {
        //if (isLobby)// потом можно сделать на проверку сцены. Если активна лобби сцена, то true
        //{
        //    CreateExampleBullet();
        //    this.enabled = false;
        //}

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

        CreateExampleBullet();

        defaultWeaponRotation = weaponTransform.localRotation;
    }

    private void CreateExampleBullet()
    {
        bulletExample = Instantiate(bulletPrefab, startBulletPosition.position, startBulletPosition.rotation, startBulletPosition);
        bulletExample.GetComponent<Bullet>().enabled = false;
        bulletExample.GetComponent<Collider>().enabled = false;
        bulletExample.transform.Find("Jet02Red").gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("CarAttack") && nextShot && !isAI)
            {
                // спавним пулю из дула пушки. /// Она уже есть(без спавна). Выстреливаем ею 
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

    // два метода вызываются из TargetDetector при обнаружении цели
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

    public void IsAI(bool value)// вызывается в installer при создании бота
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
