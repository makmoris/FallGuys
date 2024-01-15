using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private int poolCount = 2;
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
    private Collider parentBodyCollider;// у каждой машинки свой // убрать —ерриализацию
    private GameObject parentShield;// щит этой машинки, чтобы игнорировать его при выстреле. „тобы пул€ пролетала через него // убрать
    private Bumper parentBumper;

    //[Header("AI")]
    private bool isAI;
    private float shotDecisionSpeed;

    //[Header("Is lobby scene?")]
    //[SerializeField] private bool isLobby;

    private Transform target;// понадобитс€, если делать полет пули не по пр€мой, а в цель (автонаведение)


    private Quaternion defaultWeaponRotation;
    [SerializeField]private bool canAttack = true;
    [SerializeField] private bool canAIAttack;
    private bool nextShot = true;

    [SerializeField] private bool disableWeaponEvent;

    AttackTargetDetector attackTargetDetector;

    DisableWeaponBonus disableWeaponBonus;

    private List<AdditionalBulletBonus> additionalBulletBonusList = new List<AdditionalBulletBonus>();
    private bool isAutomaticAdditionalBulletBonusRecharge;

    [SerializeField]private AdditionalBulletBonus additionalBulletBonus;
    private bool isAdditionalBulletBonusSetted;

    private GameModeEnum gameMode;

    private void Awake()
    {
        damage = characteristicsData.Damage;
        rechargeTime = characteristicsData.RechargeTime;
        attackRange = characteristicsData.AttackRange;

        detectorTransform.GetComponent<AttackTargetDetector>().IsAI(isAI);

        attackTargetDetector = detectorTransform.GetComponent<AttackTargetDetector>();

        // from Start
        bulletPool = new PoolMono<Bullet>(bulletPrefab, poolCount);
        bulletPool.autoExpand = autoExpand;

        SetDetectorScale();

        CreateExampleBullet();

        //defaultWeaponRotation = weaponTransform.localRotation;
        defaultWeaponRotation = Quaternion.Euler(5f, 0f, 0f);
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

    public void Initialize(bool _isAI, Collider _bodyCollider, GameModeEnum gameMode)
    {
        parentBodyCollider = _bodyCollider;
        parentShield = parentBodyCollider.transform.GetComponentInChildren<Shield>(true).gameObject;
        parentBumper = _bodyCollider.gameObject.GetComponent<Bumper>();

        disableWeaponBonus = new(rechargeTime);

        isAI = _isAI;
        if (isAI)
        {
            attackTargetDetector.IsAI(isAI);

            ArenaDifficultyLevelsAI arenaDifficultyLevelsAI = parentBodyCollider.GetComponent<ArenaDifficultyLevelsAI>();
            if (arenaDifficultyLevelsAI != null) shotDecisionSpeed = parentBodyCollider.GetComponent<ArenaDifficultyLevelsAI>().GetShotDecisionSpeed();
        }

        this.gameMode = gameMode;
    }

    public void ChangeAttackRange(float customAttackRange)
    {
        attackRange = customAttackRange;
        SetDetectorScale();
    }

    public void ChangeRechargeTime(float customRechargeTime)
    {
        rechargeTime = customRechargeTime;
        disableWeaponBonus = new(rechargeTime);
    }

    public void ChangeDamageByPercentage(float percent)
    {
        float newDamage = damage * (percent / 100f);
        damage = newDamage;
    }

    public void EnableWeapon(GameObject gameObj)
    {
        GameObject thisGO = parentBodyCollider.gameObject;

        if (thisGO == gameObj && isAI)
        {
            disableWeaponEvent = false;
        }
        if (thisGO == gameObj && !isAI) canAttack = true;
    }
    public void DisableWeapon(GameObject gameObj)
    {
        GameObject thisGO = parentBodyCollider.gameObject;

        if (thisGO == gameObj && isAI)
        {
            canAIAttack = false;
            disableWeaponEvent = true;
        }
        if (thisGO == gameObj && !isAI) canAttack = false;
    }

    // два метода вызываютс€ из TargetDetector при обнаружении цели
    public void SetObjectForAttack(Transform objForAttackTransform)
    {
        weaponTransform.LookAt(objForAttackTransform);
        target = objForAttackTransform;
        //canAttack = true;
        if(!disableWeaponEvent) canAIAttack = true;
    }

    public void ReturnWeaponToPosition()
    {
        weaponTransform.localRotation = defaultWeaponRotation;
        //canAttack = false;
        if (!disableWeaponEvent) canAIAttack = false;
    }

    public void SetAdditionalBulletBonus(AdditionalBulletBonus additionalBulletBonus, bool showNotification)// from BonusBox
    {
        this.additionalBulletBonus = additionalBulletBonus;
        isAdditionalBulletBonusSetted = true;

        if(showNotification) parentBumper.ShowAdditionalBulletBonusNotification(additionalBulletBonus);
    }
    public void RemoveAdditionalBulletBonus()
    {
        additionalBulletBonus = null;
        isAdditionalBulletBonusSetted = false;

        parentBumper.HideAdditionalBulletBonusNotification();

        if(isAutomaticAdditionalBulletBonusRecharge) SetAdditionalBulletBonus(GetRandomAdditionalBonus(), false);
    }

    public void UseAutomaticAdditionalBulletBonusRecharge(List<AdditionalBulletBonus> additionalBulletBonusList)// from Installer
    {
        this.additionalBulletBonusList = additionalBulletBonusList;
        isAutomaticAdditionalBulletBonusRecharge = true;

        SetAdditionalBulletBonus(GetRandomAdditionalBonus(), false);
    }

    private AdditionalBulletBonus GetRandomAdditionalBonus()
    {
        int rand = Random.Range(0, additionalBulletBonusList.Count);

        return additionalBulletBonusList[rand];
    }

    private void CreateExampleBullet()
    {
        bulletExample = Instantiate(bulletPrefab, startBulletPosition.position, startBulletPosition.rotation, startBulletPosition);
        bulletExample.GetComponent<Bullet>().enabled = false;
        bulletExample.GetComponent<Collider>().enabled = false;
        bulletExample.transform.Find("FireTrail").gameObject.SetActive(false);
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

    private void ApplyDisableBonus()
    {
        parentBumper.GetBonus(disableWeaponBonus, parentBumper.gameObject);
    }

    IEnumerator Shot()
    {
        if (isAI) yield return new WaitForSeconds(shotDecisionSpeed);

        HideBulletExample();

        var bullet = bulletPool.GetFreeElement();

        bullet.SetParent(parentBodyCollider, parentShield);
        bullet.SetDamageValue(damage);
        bullet.SetGameMode(gameMode);

        bullet.transform.position = startBulletPosition.position;
        bullet.transform.rotation = startBulletPosition.rotation;

        if (isAdditionalBulletBonusSetted)
        {
            bullet.SetAdditionalBulletBonus(additionalBulletBonus, this);

            parentBumper.HideAdditionalBulletBonusNotification();
        }

        bullet.Shot();

        ApplyDisableBonus();

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
