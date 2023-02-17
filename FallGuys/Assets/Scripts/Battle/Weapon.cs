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
    private Collider parentBodyCollider;// � ������ ������� ���� // ������ �������������
    private GameObject parentShield;// ��� ���� �������, ����� ������������ ��� ��� ��������. ����� ���� ��������� ����� ���� // ������

    //[Header("AI")]
    private bool isAI;
    private float shotDecisionSpeed;

    //[Header("Is lobby scene?")]
    //[SerializeField] private bool isLobby;

    private Transform target;// �����������, ���� ������ ����� ���� �� �� ������, � � ���� (�������������)


    private Quaternion defaultWeaponRotation;
    [SerializeField]private bool canAttack = true;
    [SerializeField] private bool canAIAttack;
    private bool nextShot = true;

    [SerializeField] private bool disableWeaponEvent;

    private void Awake()
    {
        //if (isLobby)// ����� ����� ������� �� �������� �����. ���� ������� ����� �����, �� true
        //{
        //    CreateExampleBullet();
        //    this.enabled = false;
        //}

        damage = characteristicsData.Damage;
        rechargeTime = characteristicsData.RechargeTime;
        attackRange = characteristicsData.AttackRange;

        detectorTransform.GetComponent<AttackTargetDetector>().IsAI(isAI);

        // from Start
        bulletPool = new PoolMono<Bullet>(bulletPrefab, poolCount);
        bulletPool.autoExpand = autoExpand;

        SetDetectorScale();

        CreateExampleBullet();

        defaultWeaponRotation = weaponTransform.localRotation;
    }

    private void Start()
    {
        ////bulletPool = new PoolMono<Bullet>(bulletPrefab, poolCount);
        ////bulletPool.autoExpand = autoExpand;

        ////SetDetectorScale();

        ////CreateExampleBullet();

        ////defaultWeaponRotation = weaponTransform.localRotation;
    }

    private void OnEnable()
    {
        PlayerEffector.EnableWeaponEvent += EnableWeapon;
        PlayerEffector.DisableWeaponEvent += DisableWeapon;
    }

    private void CreateExampleBullet()
    {
        bulletExample = Instantiate(bulletPrefab, startBulletPosition.position, startBulletPosition.rotation, startBulletPosition);
        bulletExample.GetComponent<Bullet>().enabled = false;
        bulletExample.GetComponent<Collider>().enabled = false;
        bulletExample.transform.Find("FireTrail").gameObject.SetActive(false);
    }

    private void Update()
    {
        if (canAttack)
        {
            if (SimpleInput.GetButtonDown("CarAttack") && nextShot && !isAI)
            {
                // ������� ���� �� ���� �����. /// ��� ��� ����(��� ������). ������������ �� 
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

    private void EnableWeapon(GameObject gameObj)
    {
        GameObject thisGO = parentBodyCollider.gameObject;

        if (thisGO == gameObj && isAI)
        {
            disableWeaponEvent = false;
        }
        if (thisGO == gameObj && !isAI) canAttack = true;
    }
    private void DisableWeapon(GameObject gameObj)
    {
        GameObject thisGO = parentBodyCollider.gameObject;

        if (thisGO == gameObj && isAI)
        {
            canAIAttack = false;
            disableWeaponEvent = true;
        }
        if (thisGO == gameObj && !isAI) canAttack = false;
    }

    // ��� ������ ���������� �� TargetDetector ��� ����������� ����
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

    public void SetParentBodyCollider(Collider bodyCollider)// ���������� �� Installer �� IsAI
    {
        parentBodyCollider = bodyCollider;
        parentShield = parentBodyCollider.transform.GetComponentInChildren<Shield>(true).gameObject;
    }

    public void IsAI(bool value)// ���������� � installer ��� �������� ����
    {
        isAI = value;
        detectorTransform.GetComponent<AttackTargetDetector>().IsAI(isAI);

        shotDecisionSpeed = parentBodyCollider.GetComponent<DifficultyLevelsAI>().GetShotDecisionSpeed();
    }

    IEnumerator Shot()
    {
        if (isAI) yield return new WaitForSeconds(shotDecisionSpeed);

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

        PlayerEffector.EnableWeaponEvent -= EnableWeapon;
        PlayerEffector.DisableWeaponEvent -= DisableWeapon;
    }
}
