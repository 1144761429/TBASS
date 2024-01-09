using System;
using Characters.Player.Data;
using StackableElement;
using UnityEngine;

public enum EPlayerSpeedElementType
{
    Walk,
    Run
}

public class PlayerSpeedHandler : MonoBehaviour
{
    [field: SerializeField]
    public float Speed
    {
        get => _speedHandler.CalculateValue();
    }

    private SpeedHandler<EPlayerSpeedElementType> _speedHandler;
    private RuntimePlayerData _data;

    private void Awake()
    {
        _data = PlayerStats.Instance.RuntimeData;

        _speedHandler = new SpeedHandler<EPlayerSpeedElementType>();

        _speedHandler.Add(EPlayerSpeedElementType.Walk, new StackableElement<float>(_data.WalkSpeedBase, new IntWrapper(1),
            new IntWrapper(0), new IntWrapper(0),
            new BoolWrapper(true)));
        _speedHandler.Add(EPlayerSpeedElementType.Run,
            new StackableElement<float>(_data.SprintSpeedBase, new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
                new BoolWrapper(true)));
    }

    public StackableElement<float> GetSpeedElement(EPlayerSpeedElementType type)
    {
        return _speedHandler.Get(type);
    }

    public void DebugSpeedElements()
    {
        _speedHandler.DebugContainedStackableElement();
    }
}