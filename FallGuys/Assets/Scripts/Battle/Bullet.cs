using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : Bonus
{
    [SerializeField] float value;
    public override float Value
    {
        get => value;
        set => this.value = value;
    }

    private float time;
    public override float BonusTime
    {
        get => time;
        set => time = value;
    }

    [Space]
    [SerializeField] private float bulletSpeed = 5f;
    
    [SerializeField] private float force;
    [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;

    private Vector3 forceDirection;
    private Collider parentCollider;// ��� �������� ���� �������� ��������, �.�. ����, ��� ����� ����� ������������, ����� ��������� ������������ � �� �������� ���� ������ ����
    private GameObject parentShield;

    [SerializeField] private float effectTime;
    [SerializeField] private GameObject shotEffect;
    [SerializeField] private GameObject tail;
    private Collider bulletCollider;
    private MeshRenderer bulletMeshRenderer;

    private bool canShot;

    private bool isRing;

    private Weapon weapon;
    private AdditionalBulletBonus additionalBulletBonus;
    private bool isAdditionalBulletBonusSetted;

    private void Start()
    {
        shotEffect.SetActive(false);
        bulletCollider = gameObject.GetComponent<Collider>();
        bulletMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        canShot = false;
    }

    private void Update()
    {
        if(canShot) transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != parentCollider && other.gameObject != parentShield && !other.CompareTag("BulletIgnore"))
        {
            Explosion explosion = other.GetComponent<Explosion>();
            if (explosion != null)// ������ ��� �����
            {
                //Debug.Log("�����");
                explosion.ExplodeWithDelay();

                if (isAdditionalBulletBonusSetted)
                {
                    RemoveAdditionalBulletBonus();
                }
            }
            else // ���� ���, ������ ������ � ������ ��� ���-�� ������. 
            {
                Bumper bumper = other.GetComponent<Bumper>();
                if (bumper != null)// ���� ��� ����� - ���������� ��� � ����������� ����
                {
                    if (isAdditionalBulletBonusSetted)// ��� ������
                    {
                        bumper.GetBonus(additionalBulletBonus);
                        AdditionalBulletBonus additionalBulletBonusGO = Instantiate(additionalBulletBonus, other.transform.position, Quaternion.identity);
                        additionalBulletBonusGO.PlayEffect(other.transform.position);

                        RemoveAdditionalBulletBonus();
                    }
                    else
                    {
                        forceDirection = (other.transform.position - transform.position).normalized;
                        other.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * force, Mathf.Abs(forceDirection.y) + 3f,
                            forceDirection.z * force), forceMode);
                    }

                    // �������� ���� ����, � ���� ������, ��� � ���� �����
                    if (other != null && parentCollider != null) other.GetComponent<HitHistory>().SetLastShooter(parentCollider.gameObject);
                }
                else
                {
                    Ring ring = other.GetComponent<Ring>();
                    if(ring != null)
                    {
                        isRing = true;
                        ring.PlayerKnockedDownTheRing(GetParent());
                    }
                    else
                    {
                        isRing = false;
                    }
                }
            }

            if (other != null && parentCollider != null)
            {
                Debug.Log($"���� ������ � {other.name}. ��������� {parentCollider.gameObject.name}");

                if (isAdditionalBulletBonusSetted && !isRing)
                {
                    RemoveAdditionalBulletBonus();
                }
            }
            StartCoroutine(ShowShotEffect(effectTime));

            //gameObject.SetActive(false);
        }
    }

    public void SetAdditionalBulletBonus(AdditionalBulletBonus additionalBulletBonus, Weapon weapon)
    {
        this.additionalBulletBonus = additionalBulletBonus;
        this.weapon = weapon;
        isAdditionalBulletBonusSetted = true;
    }

    public void Shot()
    {
        canShot = true;
    }

    public void SetParent(Collider parentCollider, GameObject parentShield)
    {
        this.parentCollider = parentCollider;
        this.parentShield = parentShield;
    }

    public void SetDamageValue(float damageValue)
    {
        Value = damageValue;
    }

    // ���������� Bumper, ����� ���������, ��� �� �� ������������ �� ������ ����. ����� �� �������� �� ������ ���� 
    public GameObject GetParent()
    {
        return parentCollider.gameObject;
    }


    public override void Got()
    {
        // ��������, ����� �� ������� ����
    }

    private void RemoveAdditionalBulletBonus()
    {
        weapon.RemoveAdditionalBulletBonus();

        isAdditionalBulletBonusSetted = false;
        additionalBulletBonus = null;
        weapon = null;
    }

    IEnumerator ShowShotEffect(float time)
    {
        shotEffect.SetActive(true);
        tail.SetActive(false);
        bulletCollider.enabled = false;
        float oldSpeed = bulletSpeed;
        bulletSpeed = 0f;
        bulletMeshRenderer.enabled = false;
        canShot = false;

        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        tail.SetActive(true);
        bulletCollider.enabled = true;
        bulletSpeed = oldSpeed;
        bulletMeshRenderer.enabled = true;
        shotEffect.SetActive(false);
    }
}
