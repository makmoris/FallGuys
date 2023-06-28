public interface IPlayerData
{
    public string Name { get; }

    public bool IsAI { get; }

    public float Health { get; }
    public void SetHealth(float newValue);

    public float Speed { get; }
    public void SetSpeed(float newValue);

    public float Damage { get; }
    public void SetDamage(float newValue);
}
