using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Bullet : MonoBehaviour
{
    private HealthBonus healthBonus;

    [SerializeField] private float bulletSpeed = 5f;
    
    [SerializeField] private float force;
    [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;

    private Vector3 forceDirection;
    private Collider parentCollider;// при создании пули получаем родителя, т.е. того, кто будет пулей пользоваться, чтобы проверять столкновения и не наносить урон самому себе
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

    private GameModeEnum gameMode;

    private bool init;

    [Header("DEBUG")]
    [SerializeField] private float damage;

    private void Initialize()
    {
        shotEffect.SetActive(false);
        bulletCollider = gameObject.GetComponent<Collider>();
        bulletMeshRenderer = gameObject.GetComponent<MeshRenderer>();

        init = true;
    }

    private void Start()
    {
        if (!init)
        {
            Initialize();
        }
    }

    private void OnEnable()
    {
        if (!init)
        {
            Initialize();
        }

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
            if (explosion != null)// значит это бочка
            {
                //Debug.Log("Бочка");
                explosion.ExplodeWithDelay();

                if (isAdditionalBulletBonusSetted)
                {
                    RemoveAdditionalBulletBonus();
                }
            }
            else // если нет, значит попали в игрока или что-то другое. 
            {
                Bumper bumper = other.GetComponent<Bumper>();
                if (bumper != null)// если это игрок - Выпихиваем его в направлении пули
                {
                    bumper.GetBonus(healthBonus, this.gameObject);

                    if (isAdditionalBulletBonusSetted)// без толчка
                    {
                        bumper.GetBonus(additionalBulletBonus.GetBonus());
                        AdditionalBulletBonus additionalBulletBonusGO = Instantiate(additionalBulletBonus, other.transform.position, Quaternion.identity);
                        additionalBulletBonusGO.PlayEffect(other.transform.position, parentCollider.gameObject);

                        RemoveAdditionalBulletBonus();
                    }
                    else
                    {
                        PushCar(other.gameObject);
                    }

                    

                    // передаем инфу тому, в кого попали, кто в него попал
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

                    if (!isRing)
                    {
                        if (isAdditionalBulletBonusSetted)
                        {
                            Vector3 hitPosition = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.localPosition);

                            AdditionalBulletBonus additionalBulletBonusGO = Instantiate(additionalBulletBonus, hitPosition, Quaternion.identity);
                            additionalBulletBonusGO.PlayEffect(hitPosition, parentCollider.gameObject);

                            RemoveAdditionalBulletBonus();
                        }
                    }
                }
            }

            if (other != null && parentCollider != null)
            {
                //Debug.Log($"пуля попала в {other.name}. Выстрелил {parentCollider.gameObject.name}");

                if (isAdditionalBulletBonusSetted && !isRing)
                {
                    RemoveAdditionalBulletBonus();
                }
            }
            StartCoroutine(ShowShotEffect(effectTime));

            //gameObject.SetActive(false);
        }
    }

    public void SetGameMode(GameModeEnum gameMode)
    {
        this.gameMode = gameMode;
    }

    public void SetAdditionalBulletBonus(AdditionalBulletBonus additionalBulletBonus, Weapon weapon)
    {
        this.additionalBulletBonus = additionalBulletBonus;
        this.weapon = weapon;
        isAdditionalBulletBonusSetted = true;
    }

    public void Shot()
    {
        if (isAdditionalBulletBonusSetted)
        {
            if(additionalBulletBonus.AdditionalBulletBonusType == AdditionalBulletBonusTypeEnum.AdditionalBulletBonusBlankShot)
            {
                RemoveAdditionalBulletBonus();
                StartCoroutine(ShowShotEffect(effectTime));
                return;
            }
        }
        
        canShot = true;
    }

    public void SetParent(Collider parentCollider, GameObject parentShield)
    {
        this.parentCollider = parentCollider;
        this.parentShield = parentShield;
    }

    public void SetDamageValue(float damageValue)
    {
        damage = damageValue;
        healthBonus = new HealthBonus(damage);
    }

    // вызывается Bumper, чтобы проверять, что мы не воздействуем на самого себя. Чтобы не стрелять по самому себе 
    public GameObject GetParent()
    {
        return parentCollider.gameObject;
    }

    private void RemoveAdditionalBulletBonus()
    {
        weapon.RemoveAdditionalBulletBonus();

        isAdditionalBulletBonusSetted = false;
        additionalBulletBonus = null;
        weapon = null;
    }

    private void PushCar(GameObject carObj)
    {
        forceDirection = (carObj.transform.position - transform.position).normalized;

        if(gameMode == GameModeEnum.Race)
        {
            Rigidbody rb = carObj.GetComponent<Rigidbody>();

            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.AddForce(Vector3.up * force, forceMode);
        }
        else if(gameMode == GameModeEnum.Arena)
        {
            //carObj.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * force, forceDirection.y,
            //    force), forceMode);

            carObj.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * (force / 2f), force / 4f,
                forceDirection.z * (force / 2f)), forceMode);
        }
        else
        {
            carObj.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * force, force,
                forceDirection.z * force), forceMode);
        }
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
