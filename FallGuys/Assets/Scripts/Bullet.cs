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
    private Collider parentCollider;// ��� �������� ���� �������� ��������, �.�. ����, ��� ����� ����� ������������, ����� ��������� ������������ � �� �������� ���� ������ ����

    [SerializeField]private float force;
    private Vector3 forceDirection;
    public ForceMode forceMode = ForceMode.VelocityChange;

    private void Update()
    {
        transform.Translate(Vector3.forward * bulletSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        // ����� �����
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

        // ����� AddForce
        if (other != parentCollider)
        {
            Explosion explosion = other.GetComponent<Explosion>();
            if (explosion != null)// ������ ��� �����
            {
                Debug.Log("�����");
                explosion.ExplodeWithDelay();
            }
            else // ���� ���, ������ ������ � ������. ���������� ��� � ����������� ����
            {
                Debug.Log("�����");
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

    // ���������� Bumper, ����� ���������, ��� �� �� ������������ �� ������ ����. ����� �� �������� �� ������ ���� 
    public GameObject GetParent()
    {
        return parentCollider.gameObject;
    }
}
