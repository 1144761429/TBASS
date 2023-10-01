using UnityEngine;
using FSM;

public class EnemyAttackState : StateBase<EEnemyCombatState>
{
    private EnemyDataSO _data;
    private Animator _animator;


    public EnemyAttackState(EnemyDataSO data, Animator animator) : base(false, false)
    {
        _data = data;
        _animator = animator;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        // to be implemented
    }

    public override void OnExit()
    {
        base.OnExit();
        // to be implemented
    }
}
