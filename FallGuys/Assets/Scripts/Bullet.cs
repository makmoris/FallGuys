using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private Collider parentCollider;// при создании пули получаем родителя, т.е. того, кто будет пулей пользоваться, чтобы проверять столкновения и не наносить урон самому себе

    [SerializeField]private float force;
    private Vector3 forceDirection;
    public ForceMode forceMode = ForceMode.VelocityChange;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // через взрыв
        //if (other != parentCollider)
        //{
        //    Explosion explosion = other.GetComponent<Explosion>();
        //    if (explosion != null)
        //    {
        //        explosion.ExplodeWithDelay();
        //    }
        //    else
        //    {
        //        gameObject.GetComponent<Explosion>().ExplodeWithDelay();
        //    }
        //    gameObject.SetActive(false);
        //    Got();
        //}

        // через AddForce
        if (other != parentCollider)
        {
            Explosion explosion = other.GetComponent<Explosion>();
            if (explosion != null)// значит это бочка
            {
                Debug.Log("Бочка");
                explosion.ExplodeWithDelay();
            }
            else // если нет, значит попали в игрока. Выпихиваем его в направлении пули
            {
                Debug.Log("Игрок");
                forceDirection = (other.transform.position - transform.position).normalized;
                other.GetComponent<Rigidbody>().AddForce(new Vector3(forceDirection.x * force, Mathf.Abs(forceDirection.y) + 6f, forceDirection.z * force), forceMode);
            }

            gameObject.SetActive(false);
        }
    }

    public override void Got()
    {
        //gameObject.SetActive(false);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider != parentCollider)
    //    {
    //        gameObject.SetActive(false);
    //    }
    //}

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
}
