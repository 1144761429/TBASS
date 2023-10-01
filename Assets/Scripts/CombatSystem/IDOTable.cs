/// <summary>
/// DOT = Damage over time
/// E.g., posion damage, scorch damage, etc.
/// </summary>
public interface IDOTable
{
    public void TakeDOTDamage(float damagePerTik, float tikInterval, float duration);
}
