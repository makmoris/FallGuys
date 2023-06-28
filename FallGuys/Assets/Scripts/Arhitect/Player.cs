using UnityEngine;

public class Player : IPlayer
{
    private bool _isAI;
    public bool IsAI => _isAI;

    private float _health;
    public float Health => _health;

    private float _speed;
    public float Speed => _speed;

    private float _damage;
    public float Damage => _damage;


    private string _name;
    public string Name => _name;


    private GameObject _vehicle;
    public GameObject VehiclePrefab => _vehicle;

    private PlayerDefaultData _playerDefaultData;
    public PlayerDefaultData PlayerDefaultData => _playerDefaultData;

    private Weapon _weapon;
    public Weapon Weapon => _weapon;

    public Player(string name, GameObject vehiclePrefab, PlayerDefaultData playerDefaultData, Weapon weapon)
    {
        _isAI = false;

        _name = name;

        _vehicle = vehiclePrefab;

        _playerDefaultData = playerDefaultData;

        _weapon = weapon;


        _health = _playerDefaultData.DefaultHP;
        _speed = _playerDefaultData.DefaultSpeed;
        _damage = _playerDefaultData.DefautDamage;
    }

    
    public void SetHealth(float newValue)
    {
        _health = newValue;
    }

    public void SetSpeed(float newValue)
    {
        _speed = newValue;
    }

    public void SetDamage(float newValue)
    {
        _damage = newValue;
    }
}
