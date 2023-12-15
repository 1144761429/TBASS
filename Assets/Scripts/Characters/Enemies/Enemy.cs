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
        public abstract int Priority { get; }

        public EnemyStats Stats { get; private set; }

        public Animator Animator { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }
        [field : SerializeField] public Collider2D AlertTrigger { get; private set; }
                
        public NavMeshAgent Agent { get; private set; }
        [field : SerializeField] public List<Transform> PatrolWayPoints { get; private set; }
        

        protected virtual void Awake()
        {
            Stats = new EnemyStats(this, ID);
            
            RigidBody = GetComponent<Rigidbody2D>();
            RigidBody.gravityScale = 0;
            
            Agent = GetComponent<NavMeshAgent>();
            Agent.updateRotation = false;
            Agent.updateUpAxis = false;
            
            OnHPBelowZero += Die;
        }

        protected virtual void Update()
        {
            
        }

        private void Die()
        {
            Destroy(gameObject);
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
            
            float finalHP = Mathf.Max(Stats.CurrentHP - damage, 0);
            Stats.SetCurrentHP(finalHP);
            OnTakeDamage?.Invoke();
            AfterTakeDamage?.Invoke();

            if (finalHP <= Stats.StaticData.MinHP)
            {
                OnHPBelowZero?.Invoke();
            }
        }

        #endregion
    }
}