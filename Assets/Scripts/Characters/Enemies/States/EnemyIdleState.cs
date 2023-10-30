using FSM;
using UnityEngine;

namespace Characters.Enemies.States
{
    public class EnemyIdleState : StateBase<EEnemyPatrolState>
    {
        private Enemy _enemy;
        private EnemyStats _stats;
        private Animator _animator;
        private Rigidbody2D _rb;
        private float _timer;

        public EnemyIdleState(Enemy enemy, Animator animator, Rigidbody2D rb) : base(false, false)
        {
            _enemy = enemy;
            _stats = enemy.Stats;
            _animator = animator;
            _rb = rb;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _timer = 0;
            _rb.velocity = Vector2.zero;
        }

        public override void OnLogic()
        {
            base.OnLogic();
            _timer += Time.deltaTime;
        }

        public bool IdleTimeEnd()
        {
            return _timer >= _stats.StaticData.PatrolIdleTime;
        }
    }
}
