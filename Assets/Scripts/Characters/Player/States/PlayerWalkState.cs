using Characters.Player.Data;
using UnityEngine;
using FSM;

public class PlayerWalkState : StateBase<EPlayerMovementState>
{
    private RuntimePlayerData _data;
    private Animator _animator;
    private PlayerSpeedHandler _speedHandler;

    public PlayerWalkState(RuntimePlayerData data, Animator animator, PlayerSpeedHandler speedHandler) : base(false, false)
    {
        _data = data;
        _animator = animator;
        _speedHandler = speedHandler;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetBool(EPlayerMovementState.Walk.ToString(), true);
        _speedHandler.GetSpeedElement(EPlayerSpeedElementType.Walk).AddStack(1);
    }

    public override void OnExit()
    {
        base.OnExit();
        _animator.SetBool(EPlayerMovementState.Walk.ToString(), false);
        _speedHandler.GetSpeedElement(EPlayerSpeedElementType.Walk).RemoveStack(1);
    }
}
