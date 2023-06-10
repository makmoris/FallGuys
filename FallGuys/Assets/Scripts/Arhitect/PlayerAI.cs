using UnityEngine;

public class PlayerAI : MonoBehaviour, IPlayerAI
{
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

    private GameObject _vehicle;
    public GameObject Vehicle => _vehicle;

    private Weapon _weapon;
    public Weapon Weapon => _weapon;

    public PlayerAI(PlayerDefaultData data, GameObject vehiclePrefab, Weapon weapon)
    {
        _health = data.DefaultHP;
        _speed = data.DefaultSpeed;
        _damage = data.DefautDamage;

        _vehicle = vehiclePrefab;

        _weapon = weapon;
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
        Destroy(_vehicle);
    }
}
