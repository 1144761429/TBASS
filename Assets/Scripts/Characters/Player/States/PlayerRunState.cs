using Characters.Player.Data;
using UnityEngine;
using FSM;

public class PlayerRunState : StateBase<EPlayerMovementState>
{
    private RuntimePlayerData _data;
    private Animator _animator;

    private PlayerSpeedHandler _speedHandler;

    public PlayerRunState(RuntimePlayerData data, Animator animator, PlayerSpeedHandler speedHandler) : base(false, false)
    {
        _data = data;
        _animator = animator;
        _speedHandler = speedHandler;
    }

    public override void OnEnter()
    {
        base.OnEnter();
        _animator.SetBool(EPlayerMovementState.Run.ToString(), true);
        _speedHandler.GetSpeedElement(EPlayerSpeedElementType.Run).AddStack(1);
    }

    public override void OnExit()
    {
        base.OnExit();
        _animator.SetBool(EPlayerMovementState.Run.ToString(), false);
        _speedHandler.GetSpeedElement(EPlayerSpeedElementType.Run).RemoveStack(1);
    }
}