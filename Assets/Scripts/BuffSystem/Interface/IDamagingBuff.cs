namespace BuffSystem.Interface
{
    public interface IDamagingBuff
    {
        IDamageable DamageableTarget { get; }
        float Damage { get; }
    }
}