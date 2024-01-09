using System;
using System.Collections.Generic;
using AbilitySystem;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityTimer;


public class Test : MonoBehaviour, IDamageable
{
    public event Action BeforeTakeDamage;
    public event Action OnTakeDamage;
    public event Action AfterTakeDamage;
    public event Action OnHPBelowZero;

    public GameObject Entity => gameObject;

    public int TargetPriority => 2;

    public Transform[] possibleTragets { get;set; }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}