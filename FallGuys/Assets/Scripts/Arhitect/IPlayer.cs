using UnityEngine;

public interface IPlayer
{
    public float Health { get; }
    public float Speed { get; }
    public float Damage { get; }

    public GameObject Vehicle { get; }

    public Weapon Weapon { get; }

    public void SetHealth(float newValue);
    public void SetSpeed(float newValue);
    public void SetDamage(float newValue);
}
