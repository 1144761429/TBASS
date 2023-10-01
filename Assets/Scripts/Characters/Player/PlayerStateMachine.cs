using UnityEngine;
using FSM;

public enum EPlayerState
{
    Idle,
    Move
}

public enum EPlayerMovementState
{
    Walk,
    Run
}

public class PlayerStateMachine : MonoBehaviour
{
    private PlayerDataSO _data;
    private Animator _animator;
    private PlayerSpeedHandler _speedHandler;
    private PlayerInputHandler _inputHandler;

    private StateMachine<EPlayerState> _superSM;

    private StateMachine<EPlayerState, EPlayerMovementState, string> _movementSM;
    private PlayerWalkState _walkState;
    private PlayerRunState _runState;

    private PlayerIdleState _idleState;


    private void Awake()
    {
        _data = PlayerStats.Instance.RuntimeData;

        _animator = GetComponentInChildren<Animator>();
        _speedHandler = GetComponent<PlayerSpeedHandler>();
        _inputHandler = GetComponent<PlayerInputHandler>();

        _superSM = new StateMachine<EPlayerState>(false);

        _movementSM = new StateMachine<EPlayerState, EPlayerMovementState, string>(false);
        _walkState = new PlayerWalkState(_data, _animator, _speedHandler);
        _runState = new PlayerRunState(_data, _animator, _speedHandler);

        _idleState = new PlayerIdleState(_data, _animator);

        _movementSM.AddState(EPlayerMovementState.Walk, _walkState);
        _movementSM.AddState(EPlayerMovementState.Run, _runState);
        _movementSM.AddTwoWayTransition(new Transition<EPlayerMovementState>(EPlayerMovementState.Walk,
            EPlayerMovementState.Run,
            transition => _inputHandler.IsHoldingSprint));
        _movementSM.SetStartState(EPlayerMovementState.Walk);

        _superSM.AddState(EPlayerState.Idle, _idleState);
        _superSM.AddState(EPlayerState.Move, _movementSM);
        _superSM.AddTwoWayTransition(new Transition<EPlayerState>(EPlayerState.Idle, EPlayerState.Move,
            transition => _inputHandler.MovementInput.magnitude != 0));

        _superSM.SetStartState(EPlayerState.Idle);
        _superSM.Init();
    }

    private void Update()
    {
        _superSM.OnLogic();
    }
}