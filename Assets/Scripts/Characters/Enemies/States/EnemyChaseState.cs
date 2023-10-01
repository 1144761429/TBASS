using UnityEngine;
using FSM;

public class EnemyChaseState : StateBase<EEnemyCombatState>
{
    private EnemyDataSO _data;
    private Animator _animator;
    private Transform _enemyTransform;
    private Transform _playerTransfrom;
    private EnemySpeedHandler _speedHandler;

    public EnemyChaseState(EnemyDataSO data, Animator animator, Transform enemyTransform, EnemySpeedHandler speedHandler) : base(false, false)
    {
        _data = data;
        _animator = animator;
        _enemyTransform = enemyTransform;
        _playerTransfrom = GameObject.FindGameObjectWithTag("Player").transform;
        _speedHandler = speedHandler;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _speedHandler.GetSpeedElement(EEnemySpeedElementType.Chase).AddStack(1);
    }

    public override void OnExit()
    {
        base.OnExit();
        _speedHandler.GetSpeedElement(EEnemySpeedElementType.Chase).RemoveStack(1);
    }

    public bool PlayerIsInAttackRange()
    {
        return Vector2.Distance(_enemyTransform.position, _playerTransfrom.position) < _data.AttackRange;
    }

    // public override void CheckForTransition()
    // {
    //     base.CheckForTransition();
    //     if (_enemy.EnemyDistance.DistanceToPlayer > _data.PatrolAlertDistance)
    //     {
    //         //_stateMachine.ChangeState(_enemy.States.Walk);
    //     }
    // }

    // public override void Enter()
    // {
    //     base.Enter();
    // }

    // public override void Exit()
    // {
    //     base.Exit();
    // }

    // public override void LogicUpdate()
    // {
    //     base.LogicUpdate();
    //     //_enemy.RB.velocity = (_currentTargetWayPoint.position - _enemy.transform.position).normalized * _data.PatrolSpeed;
    // }

    // public override void PhysicUpdate()
    // {
    //     base.PhysicUpdate();
    // }
}
