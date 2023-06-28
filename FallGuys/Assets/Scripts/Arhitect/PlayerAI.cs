using UnityEngine;

public class PlayerAI : IPlayerAI
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


    private Material _vehicleColorMaterial;
    public Material VehicleColorMaterial => _vehicleColorMaterial;

    private Weapon _weapon;
    public Weapon Weapon => _weapon;

    public PlayerAI(string name, Material vehicleColorMaterial, Weapon weapon)
    {
        _isAI = true;

        _name = name;

        _vehicleColorMaterial = vehicleColorMaterial;

        _weapon = weapon;
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

    public void SetDefaultData(PlayerDefaultData playerDefaultData)
    {
        _health = playerDefaultData.DefaultHP;
        _speed = playerDefaultData.DefaultSpeed;
        _damage = playerDefaultData.DefautDamage;
    }
}
