using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.States
{
    public class EnemyChaseState : StateBase<EEnemyCombatState>
    {
        private Enemy _enemy;
        private EnemyStats _stats;
        
        private Animator _animator;
        private Transform _enemyTransform;
        private NavMeshAgent _agent;
        
        private EnemySpeedHandler _speedHandler;
        private Transform _playerTransfrom;
        

        public EnemyChaseState(Enemy enemy) : base(false, false)
        {
            _enemy = enemy;
            
            _stats = enemy.Stats;
            _animator = enemy.Animator;
            _enemyTransform = enemy.transform;
            _agent = enemy.Agent;
            
            _speedHandler = enemy.Stats.SpeedHandler;
            _playerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            _speedHandler.GetSpeedElement(EEnemySpeedElementType.Chase).AddStack(1);
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            _agent.stoppingDistance = 0.2f;
            _agent.speed = _speedHandler.Speed;
            _agent.destination = _playerTransfrom.position;
        }

        public override void OnExit()
        {
            base.OnExit();
            _speedHandler.GetSpeedElement(EEnemySpeedElementType.Chase).RemoveStack(1);
        }

        public bool PlayerIsInAttackRange()
        {
            return Vector2.Distance(_enemyTransform.position, _playerTransfrom.position) < _stats.StaticData.AttackRange;
        }
    }
}
