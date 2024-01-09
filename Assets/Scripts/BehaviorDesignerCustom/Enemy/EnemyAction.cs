using BehaviorDesigner.Runtime.Tasks;
using Characters.Enemies;
using Characters.Enemies.SerializableData;
using UnityEngine;


namespace BehaviorDesignerCustom.Enemy
{
    public abstract class EnemyAction : Action
    {
        protected Characters.Enemies.Enemy enemy;
        protected GameObject enemyEntity;
        protected IDamageable damageable;
        protected EnemyStats stats;
        protected EnemyData staticData;
        protected Animator animator;
        protected Rigidbody2D rigidbody;
        
        
        public override void OnAwake()
        {
            enemy = GetComponent<Characters.Enemies.Enemy>();
            enemyEntity = gameObject;
            damageable = enemy;
            stats = GetComponent<EnemyStats>();
            staticData = stats.StaticData;
            animator = enemy.Animator;
            rigidbody = enemy.RigidBody;
        }
    }
}