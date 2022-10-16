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
    public float rechargeTime;// ����� ��������. ������, ���� ����������� ����������� - ����� ����� ��������
    public float attackRange;// ��������� - ��� ����� �� x � z. �� y ������ ������ ���� �������, ����� �������� ���

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
                // ������� ���� �� ���� �����. /// ��� ��� ����(��� ������). ������������ �� 
                StartCoroutine(Shot());
                nextShot = false;
                //bullet.GetComponent<Bullet>().Shot();
                // ������������ ��������� ��� notReacharge
            }
        }
    }

    // ��� ������ ���������� �� TargetDetector ��� ����������� ����
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

        // ������� ������������ ���������� �������� � ������������� � ����� �����, ���� ���. ����� �� ������ ��� ������ ���
        bullet.SetParent(parentBodyCollider);
        bullet.SetDamageValue(damage);

        bullet.transform.position = startBulletPosition.position;
        bullet.transform.rotation = startBulletPosition.rotation;
        yield return new WaitForSeconds(rechargeTime);
        nextShot = true;
    }
}
