using UnityEngine;
using FSM;

public class EnemyWalkState : StateBase<EEnemyPatrolState>
{
    private EnemyDataSO _data;
    private Animator _animator;
    private Rigidbody2D _rb;
    private Transform _enemyTransfrom;
    private EnemySpeedHandler _speedHandler;
    private Transform _currentTargetWayPoint;

    public EnemyWalkState(EnemyDataSO data, Animator animator, Rigidbody2D rb, Transform transform, EnemySpeedHandler speedHandler) : base(false, false)
    {
        _data = data;
        _animator = animator;
        _rb = rb;
        _enemyTransfrom = transform;
        _speedHandler = speedHandler;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _currentTargetWayPoint = _data.PatrolWayPoints[_data.CurrentWayPoint];
        _speedHandler.GetSpeedElement(EEnemySpeedElementType.Patrol).AddStack(1);
    }

    public override void OnLogic()
    {
        base.OnLogic();
        _rb.velocity = (_currentTargetWayPoint.position - _enemyTransfrom.position).normalized * _data.PatrolSpeed;
    }

    public override void OnExit()
    {
        base.OnExit();
        if (ReachedWayPoint())
        {
            _data.SetWayPointToNext();
        }

        _speedHandler.GetSpeedElement(EEnemySpeedElementType.Patrol).RemoveStack(1);
    }

    public bool ReachedWayPoint()
    {
        return Vector2.Distance(_enemyTransfrom.position, _currentTargetWayPoint.position) < 0.05f;
    }
}
