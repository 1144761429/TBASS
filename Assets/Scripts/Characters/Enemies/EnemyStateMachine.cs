using UnityEngine;
using FSM;

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

public class EnemyStateMachine : MonoBehaviour
{
    public string CurrentStateName { get => _superSM.ActiveStateName.ToString(); }

    [SerializeField] private EnemyDataSO _data;
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

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _speedHandler = GetComponent<EnemySpeedHandler>();
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;



        _patrolSM = new StateMachine<EEnemyState, EEnemyPatrolState, string>(false);
        _idleState = new EnemyIdleState(_data, _animator, _rb);
        _walkState = new EnemyWalkState(_data, _animator, _rb, transform, _speedHandler);
        _patrolSM.AddState(EEnemyPatrolState.Idle, _idleState);
        _patrolSM.AddState(EEnemyPatrolState.Walk, _walkState);
        _patrolSM.AddTransition(new Transition<EEnemyPatrolState>(EEnemyPatrolState.Idle, EEnemyPatrolState.Walk,
            transition => _idleState.IdleTimeEnd()));
        _patrolSM.AddTransition(new Transition<EEnemyPatrolState>(EEnemyPatrolState.Walk, EEnemyPatrolState.Idle,
            transition => _walkState.ReachedWayPoint()));
        _patrolSM.SetStartState(EEnemyPatrolState.Idle);

        _combatSM = new StateMachine<EEnemyState, EEnemyCombatState, string>(false);
        _chaseState = new EnemyChaseState(_data, _animator, transform, _speedHandler);
        _attackState = new EnemyAttackState(_data, _animator);
        _combatSM.AddState(EEnemyCombatState.Chase, _chaseState);
        _combatSM.AddState(EEnemyCombatState.Attack, _attackState);
        _combatSM.AddTwoWayTransition(new Transition<EEnemyCombatState>(EEnemyCombatState.Chase, EEnemyCombatState.Attack,
            transition => _chaseState.PlayerIsInAttackRange()));
        _combatSM.SetStartState(EEnemyCombatState.Chase);

        _superSM = new StateMachine<EEnemyState>(false);
        _superSM.AddState(EEnemyState.Patrol, _patrolSM);
        _superSM.AddState(EEnemyState.Combat, _combatSM);
        _superSM.AddTwoWayTransition(EEnemyState.Patrol, EEnemyState.Combat,
            transition => Vector2.Distance(_playerTransform.position, transform.position) <= _data.PatrolAlertDistance);
    }

    private void Start()
    {
        _combatSM.Init();
        _patrolSM.Init();
        _superSM.Init();
    }

    private void Update()
    {
        _superSM.OnLogic();
    }
}
