using UnityEngine;
using FSM;

public class EnemyIdleState : StateBase<EEnemyPatrolState>
{
    private EnemyDataSO _data;
    private Animator _animator;
    private Rigidbody2D _rb;
    private float _timer;

    public EnemyIdleState(EnemyDataSO data, Animator animator, Rigidbody2D rb) : base(false, false)
    {
        _data = data;
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
        return _timer >= _data.PatrolIdleTime;
    }
    // public override void CheckForTransition()
    // {
    //     base.CheckForTransition();
    //     if (_timer > (_startTime + _data.PatrolIdleTime))
    //     {
    //         _stateMachine.ChangeState(_enemy.States.Walk);
    //     }
    // }
}
