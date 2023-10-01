using System;
using System.Diagnostics.Contracts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Data/Enemy")]
public class EnemyDataSO : ScriptableObject
{
    public float CurrentHp { get; set; }
    [field: SerializeField] public float MaxHp { get; private set; }
    [field: SerializeField] public float MinHp { get; private set; }
    public bool CanDie { get; private set; }

    [field: SerializeField] public float PatrolIdleTime { get; private set; }
    [field: SerializeField] public float PatrolAlertDistance { get; private set; }
    [field: SerializeField] public Transform[] PatrolWayPoints { get; set; }
    [field: SerializeField] public int CurrentWayPoint { get; private set; }
    [field: SerializeField] public float PatrolSpeed { get; private set; }
    [field: SerializeField] public float ChaseSpeed { get; private set; }

    [field: SerializeField] public float AttackRange { get; private set; }

    public void Init()
    {
        CurrentHp = MaxHp;
    }

    public void Init(float initialHp)
    {
        CurrentHp = MaxHp;
    }

    public void SetWayPointToNext()
    {
        CurrentWayPoint = CurrentWayPoint + 1 < PatrolWayPoints.Length ? CurrentWayPoint + 1 : 0;
    }
}