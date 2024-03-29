using Characters.Player.Data;
using UnityEngine;
using FSM;
public class PlayerIdleState : StateBase<EPlayerState>
{
    private RuntimePlayerData _data;
    private Animator _animator;

    public PlayerIdleState(RuntimePlayerData data, Animator animator) : base(false, false)
    {
        _data = data;
        _animator = animator;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetBool(EPlayerState.Idle.ToString(), true);
    }

    public override void OnExit()
    {
        base.OnExit();
        _animator.SetBool(EPlayerState.Idle.ToString(), false);
    }
}
