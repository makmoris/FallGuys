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

    [Space]
    [SerializeField] private float bulletSpeed = 5f;
    
    [SerializeField] private float force;
    [SerializeField] private ForceMode forceMode = ForceMode.VelocityChange;

    private Vector3 forceDirection;
    private Collider parentCollider;// при создании пули получаем родителя, т.е. того, кто будет пулей пользоваться, чтобы проверять столкновения и не наносить урон самому себе

    [SerializeField] private float effectTime;
    [SerializeField] private GameObject shotEffect;
    private Collider bulletCollider;
    private MeshRenderer bulletMeshRenderer;

    private void Start()
    {
        shotEffect.SetActive(false);
        bulletCollider = gameObject.GetComponent<Collider>();
        bulletMeshRenderer = gameObject.GetComponent<MeshRenderer>();
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other != parentCollider)
        {
            Explosion explosion = other.GetComponent<Explosion>();
            if (explosion != null)// значит это бочка
            {
                Debug.Log("Бочка");
                explosion.ExplodeWithDelay();

                gameObject.SetActive(false);
            }
            else // если нет, значит попали в игрока или что-то другое. 
            {
                Bumper bumper = other.GetComponent<Bumper>();
                if (bumper != null)// если это игрок - Выпихиваем его в направлении пули
                {
                    Debug.Log("Игрок");
                    forceDirection = (other.transform.position - transform.position).normalized;
                    other.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * force, Mathf.Abs(forceDirection.y) + 6f, 
                        forceDirection.z * force), forceMode);
                }

                StartCoroutine(ShowShotEffect(effectTime));
            }
            
            //gameObject.SetActive(false);
        }
    }

    public void SetParent(Collider parentCollider)
    {
        this.parentCollider = parentCollider;
    }

    public void SetDamageValue(float damageValue)
    {
        Value = damageValue;
    }

    // вызывается Bumper, чтобы проверять, что мы не воздействуем на самого себя. Чтобы не стрелять по самому себе 
    public GameObject GetParent()
    {
        return parentCollider.gameObject;
    }


    public override void Got()
    {
        // заглушка, чтобы не удалять пулю
    }

    IEnumerator ShowShotEffect(float time)
    {
        shotEffect.SetActive(true);
        bulletCollider.enabled = false;
        float oldSpeed = bulletSpeed;
        bulletSpeed = 0f;
        bulletMeshRenderer.enabled = false;

        yield return new WaitForSeconds(time);

        gameObject.SetActive(false);
        bulletCollider.enabled = true;
        bulletSpeed = oldSpeed;
        bulletMeshRenderer.enabled = true;
        shotEffect.SetActive(false);
    }
}
