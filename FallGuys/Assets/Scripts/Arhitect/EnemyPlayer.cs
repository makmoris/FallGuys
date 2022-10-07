using UnityEngine;

public class EnemyPlayer : MonoBehaviour, IEnemyPlayer
{
    // implement interfaces
    private GameObject _enemyObject;
    public GameObject EnemyObject
    {
        get { return _enemyObject; }
        private set { _enemyObject = value; }
    }

    private float _health;
    public float Health
    {
        get { return _health; }
        private set { _health = value; }
    }

    private float _speed;
    public float Speed
    {
        get { return _speed; }
        private set { _speed = value; }
    }

    private float _damage;
    public float Damage
    {
        get { return _damage; }
        private set { _damage = value; }
    }


    public EnemyPlayer(GameObject obj)
    {
        EnemyObject = obj;
    }


    public void SetHealth(float newValue)
    {
        Health = newValue;
    }

    public void SetSpeed(float newValue)
    {
        Speed = newValue;
    }

    public void SetDamage(float newValue)
    {
        Damage = newValue;
    }

    public void Destroy()
    {
        Destroy(EnemyObject);
    }
}
