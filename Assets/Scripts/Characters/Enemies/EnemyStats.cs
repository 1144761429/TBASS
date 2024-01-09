using System;
using Characters.Enemies.Data;
using Characters.Enemies.SerializableData;
using UISystem;
using UnityEngine;

namespace Characters.Enemies
{
    /// <summary>
    /// A class that stores the static data and runtime data of an enemy.
    /// </summary>
    public abstract class EnemyStats : MonoBehaviour
    {
        public event EventHandler<HealthChangeEventArgs> OnHealthChange;
        
        // TODO: Add the following events to methods.
        public event EventHandler<HealthChangeEventArgs> OnHealthIncrease;
        public event EventHandler<HealthChangeEventArgs> OnHealthDecrease;
        
        protected Enemy _enemy;
        [field: SerializeField] public int ID { get; private set; }
        public String Name { get; private set; }
        
        public abstract EnemyData StaticData { get; protected set; }
        public abstract RuntimeEnemyData RuntimeData { get; protected set; }

        public void Awake(){
             InitializeData();   
        }

        public void AddHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Health amount to add must be non-negative.", nameof(amount));
            }

            float healthBeforeChange = RuntimeData.CurrentHealth;
        
            overflowAmount = Mathf.Max(0, RuntimeData.CurrentHealth + amount - StaticData.MaxHP);
            float actualAmountToAdd = amount - overflowAmount;
            RuntimeData.AddHealth(actualAmountToAdd);

            HealthChangeEventArgs args =
                new HealthChangeEventArgs(healthBeforeChange, RuntimeData.CurrentHealth, StaticData.MaxHP);

            if (triggerHpChangeEvent) OnHealthChange?.Invoke(this, args);
        }
        
        public void ReduceHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
        {
            if (amount < 0)
            {
                throw new ArgumentException("Health amount to reduce must be non-negative.", nameof(amount));
            }

            float healthBeforeChange = RuntimeData.CurrentHealth;
        
            overflowAmount = Mathf.Min(0, RuntimeData.CurrentHealth - StaticData.MinHP - amount);
            float actualAmountToReduce = amount - overflowAmount;
            RuntimeData.ReduceHealth(actualAmountToReduce);

            HealthChangeEventArgs args =
                new HealthChangeEventArgs(healthBeforeChange, RuntimeData.CurrentHealth, StaticData.MaxHP);
        
            if (triggerHpChangeEvent) OnHealthChange?.Invoke(this, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// protected bool IsValidID(){
        ///     return true;
        /// }

        /// <summary>
        /// Initialize static data and create runtime data in this method.
        /// </summary>
        protected abstract void InitializeData();
    }
}
