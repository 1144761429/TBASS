using System.Collections.Generic;
using Characters.Enemies.States;
using UnityEngine;
using FSM;

namespace Characters.Enemies
{
    public enum EEnemyState
    {
        Patrol,
        Combat
    }

    public enum EEnemyPatrolState
    {
        Idle,
        Walk
    }

    public enum EEnemyCombatState
    {
        Chase,
        Attack
    }

    public class EnemyStateMachine_PatrolRioter
    {
        private Enemy _enemy;
        private EnemyStats _stats;

        public string CurrentStateName
        {
            get => _superSM.ActiveStateName.ToString();
        }

        private Animator _animator;
        private Rigidbody2D _rb;
        private EnemySpeedHandler _speedHandler;
        private Transform _playerTransform;

        private StateMachine<EEnemyState> _superSM;

        private StateMachine<EEnemyState, EEnemyPatrolState, string> _patrolSM;
        private EnemyIdleState _idleState;
        private EnemyWalkState _walkState;

        private StateMachine<EEnemyState, EEnemyCombatState, string> _combatSM;
        private EnemyChaseState _chaseState;
        private EnemyAttackState _attackState;

        public EnemyStateMachine_PatrolRioter(Enemy enemy)
        {
            _enemy = enemy;
            _stats = enemy.Stats;

            _animator = _enemy.Animator;
            _rb = _enemy.RigidBody;
            _playerTransform = PlayerStats.Instance.gameObject.transform;


            _speedHandler = _enemy.Stats.SpeedHandler;

            _patrolSM = new StateMachine<EEnemyState, EEnemyPatrolState, string>(false);
            _idleState = new EnemyIdleState(_enemy, _animator, _rb);
            _walkState = new EnemyWalkState(_enemy);
            _patrolSM.AddState(EEnemyPatrolState.Idle, _idleState);
            _patrolSM.AddState(EEnemyPatrolState.Walk, _walkState);
            _patrolSM.AddTransition(new Transition<EEnemyPatrolState>(EEnemyPatrolState.Idle, EEnemyPatrolState.Walk,
                transition => _idleState.IdleTimeEnd() && _enemy.PatrolWayPoints.Count > 0));
            _patrolSM.AddTransition(new Transition<EEnemyPatrolState>(EEnemyPatrolState.Walk, EEnemyPatrolState.Idle,
                transition => _walkState.ReachedWayPoint()));
            _patrolSM.SetStartState(EEnemyPatrolState.Idle);

            _combatSM = new StateMachine<EEnemyState, EEnemyCombatState, string>(false);
            _chaseState = new EnemyChaseState(_enemy);
            _attackState = new EnemyAttackState(_enemy, _animator);
            _combatSM.AddState(EEnemyCombatState.Chase, _chaseState);
            _combatSM.AddState(EEnemyCombatState.Attack, _attackState);
            _combatSM.AddTwoWayTransition(new Transition<EEnemyCombatState>(EEnemyCombatState.Chase,
                EEnemyCombatState.Attack,
                transition => Vector2.Distance(_enemy.transform.position, _playerTransform.position) <=
                              _stats.StaticData.AttackRange));
            _combatSM.SetStartState(EEnemyCombatState.Chase);

            _superSM = new StateMachine<EEnemyState>(false);
            _superSM.AddState(EEnemyState.Patrol, _patrolSM);
            _superSM.AddState(EEnemyState.Combat, _combatSM);
            _superSM.AddTwoWayTransition(EEnemyState.Patrol, EEnemyState.Combat,
                transition => Vector2.Distance(_playerTransform.position, _enemy.transform.position) <=
                              _stats.StaticData.AlertDistance);
        }

        public void Init()
        {
            _combatSM.Init();
            _patrolSM.Init();
            _superSM.Init();
        }

        public void UpdateStateMachine()
        {
            _superSM.OnLogic();
        }
    }
}