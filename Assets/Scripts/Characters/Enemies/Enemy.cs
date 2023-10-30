using System;
using System.Collections.Generic;
using BuffSystem.Common;
using UnityEngine;

namespace Characters.Enemies
{
    public abstract class Enemy : MonoBehaviour, IBuffable, IDamageable
    {
        public abstract int ID { get; }
        public EnemyStats Stats { get; private set; }

        public Animator Animator { get; private set; }
        public Rigidbody2D RigidBody { get; private set; }

        [field : SerializeField] public List<Transform> PatrolWayPoints { get; private set; }
        

        protected virtual void Awake()
        {
            RigidBody = GetComponent<Rigidbody2D>();
            RigidBody.gravityScale = 0;

            Stats = new EnemyStats(this, ID);

            OnHPBelowZero += Die;
        }

        protected virtual void Update()
        {
            Stats.SpeedHandler.Debug();
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