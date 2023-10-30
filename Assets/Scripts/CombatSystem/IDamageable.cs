using System;

public interface IDamageable
{
    public event Action BeforeTakeDamage; 
    public event Action OnTakeDamage; 
    public event Action AfterTakeDamage;
    public event Action OnHPBelowZero;
    
    public void TakeDamage(float damage);
    
}