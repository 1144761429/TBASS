using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.AI;

public class TestTwo : MonoBehaviour, IDamageable
{
    public event Action BeforeTakeDamage;
    public event Action OnTakeDamage;
    public event Action AfterTakeDamage;
    public event Action OnHPBelowZero;

    public GameObject Entity => gameObject;

    public int Priority => 3;

    public NavMeshAgent agent;
    public BoxCollider2D c;
    private void Awake()
    {
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    public void TakeDamage(float damage)
    {
        throw new NotImplementedException();
    }
}