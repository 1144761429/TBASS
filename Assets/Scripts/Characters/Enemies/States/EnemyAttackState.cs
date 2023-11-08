using FSM;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class EnemyAttackState : StateBase<EEnemyCombatState>
    {
        private Enemy _enemy;
        private EnemyStats _stats;
        private Animator _animator;


        public EnemyAttackState(Enemy enemy, Animator animator) : base(false, false)
        {
            _enemy = enemy;
            _stats = enemy.Stats;
            _animator = animator;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            // to be implemented===========
        }

        public override void OnLogic()
        {
            base.OnLogic();

            if (Time.time >= _stats.LastAttackTime + _stats.StaticData.AttackCooldown)
            {
                Debug.Log("Attacking");
                _stats.SetLastAttackTime(Time.time);
            }
            
        }

        public override void OnExit()
        {
            base.OnExit();
            // to be implemented
        }
    }
}
