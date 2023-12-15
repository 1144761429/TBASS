using System;
using UnityEngine;

public interface IDamageable
{
    public event Action BeforeTakeDamage; 
    public event Action OnTakeDamage; 
    public event Action AfterTakeDamage;
    public event Action OnHPBelowZero;
    
    public GameObject Entity { get; }
    public int Priority { get; }
    public void TakeDamage(float damage);
}