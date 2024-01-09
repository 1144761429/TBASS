using System;
using System.Collections.Generic;
using BuffSystem.Common;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies
{
    public abstract class Enemy : MonoBehaviour, IBuffable, IDamageable
    {
        public abstract int ID { get; }

        public GameObject Entity => gameObject;
        public abstract int TargetPriority { get; }

        public EnemyStats Stats { get; set; }

        public Animator Animator { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        [field : SerializeField] public Collider2D AlertTrigger { get; private set; }
                
        public NavMeshAgent Agent { get; private set; }
        [field : SerializeField] public List<Transform> PatrolWayPoints { get; private set; }
        

        protected virtual void Awake()
        {
            Stats = GetComponent<EnemyStats>();
            
            RigidBody = GetComponent<Rigidbody2D>();
            RigidBody.gravityScale = 0;
            
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
        }

        #region IBuffable

        public bool CanTakeBuff  { get; protected set; }

        public bool IsBleedResist { get; protected set; }

        public BuffHandler BuffHandler { get; protected set; }

        #endregion

        #region IDamageable

        public event Action BeforeTakeDamage;
        public event Action OnTakeDamage;
        public event Action AfterTakeDamage;
        public event Action OnHPBelowZero;

        public void TakeDamage(float damage)
        {
            BeforeTakeDamage?.Invoke();
            
            Stats.ReduceHp(damage, out float overflowAmount);
            OnTakeDamage?.Invoke();
            AfterTakeDamage?.Invoke();

            if (damage <= Stats.StaticData.MinHP)
            {
                OnHPBelowZero?.Invoke();
            }
        }

        #endregion
    }
}