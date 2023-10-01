using System.Collections;
using System.Collections.Generic;
using System;
using System.Timers;
using BuffSystem.Common;
using BuffSystem.Interface;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class EnemyStats : MonoBehaviour, IDamageable, IBuffable
{
    public event Action BeforeTakeDamageCallback;
    public event Action TakeDamageCallback;
    public event Action AfterTakeDamageCallback;

    public bool CanTakeBuff => true;
    public bool IsBleedResist => false;
    public BuffHandler BuffHandler { get; private set; }

    [SerializeField] private EnemyDataSO sourceData;
    public EnemyDataSO RuntimeData { get; private set; }

    private Rigidbody2D _rigidBody;

    #region Monobehavior Methods

    private void Awake()
    {
        BuffHandler = new BuffHandler();
        RuntimeData = Instantiate<EnemyDataSO>(sourceData);
        RuntimeData.Init();

        _rigidBody = GetComponent<Rigidbody2D>();
        _rigidBody.gravityScale = 0;
    }

    #endregion

    public void TakeDamage(float damage)
    {
        BeforeTakeDamageCallback?.Invoke();

        float finalHp = Mathf.Max(RuntimeData.CurrentHp - damage, RuntimeData.MinHp);
        RuntimeData.CurrentHp = finalHp;
        TakeDamageCallback?.Invoke();
        AfterTakeDamageCallback?.Invoke();

        if (finalHp <= RuntimeData.MinHp)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(this.gameObject);
    }
}