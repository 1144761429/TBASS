using FSM;
using UnityEngine;
using UnityEngine.AI;

namespace Characters.Enemies.States
{
    public class EnemyWalkState : StateBase<EEnemyPatrolState>
    {
        private Enemy _enemy;
        private EnemyStats _stats;
        
        private Animator _animator;
        private Rigidbody2D _rb;
        private Transform _enemyTransfrom;
        private NavMeshAgent _agent;
        
        private EnemySpeedHandler _speedHandler;
        private Transform _currentTargetWayPoint;

        public EnemyWalkState(Enemy enemy) : base(false, false)
        {
            _enemy = enemy;
            
            _stats = enemy.Stats;
            _animator = enemy.Animator;
            _enemyTransfrom = _enemy.transform;
            _rb = _enemy.RigidBody;
            _agent = enemy.Agent;
                
            _speedHandler = _enemy.Stats.SpeedHandler;
        }

        public override void OnEnter()
        {
            base.OnEnter();
            
            _speedHandler.GetSpeedElement(EEnemySpeedElementType.Patrol).AddStack(1);
            
            _currentTargetWayPoint = _enemy.PatrolWayPoints[_stats.CurrentWayPointIndex];
        }

        public override void OnLogic()
        {
            base.OnLogic();
            
            // _rb.velocity = (_currentTargetWayPoint.position - _enemyTransfrom.position).normalized *
            //                _stats.SpeedHandler.Speed;

            _agent.stoppingDistance = 0.2f;
            _agent.speed = _speedHandler.Speed;
            _agent.destination = _currentTargetWayPoint.position;
        }

        public override void OnExit()
        {
            base.OnExit();
            
            if (ReachedWayPoint())
            {
                if (_stats.CurrentWayPointIndex + 1 >= _enemy.PatrolWayPoints.Count)
                {
                    _stats.SetCurrentWayPointIndex(0);
                }
                else
                {
                    _stats.SetCurrentWayPointIndex(_stats.CurrentWayPointIndex + 1);
                }
            }

            _speedHandler.GetSpeedElement(EEnemySpeedElementType.Patrol).RemoveStack(1);
        }

        public bool ReachedWayPoint()
        {
            return Vector2.Distance(_enemyTransfrom.position, _currentTargetWayPoint.position) < 0.05f;
        }
    }
}
