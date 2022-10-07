public interface IPlayer
{
    public float Health { get; }
    public float Speed { get; }
    public float Damage { get; }

    public void SetHealth(float newValue);
    public void SetSpeed(float newValue);
    public void SetDamage(float newValue);
}
