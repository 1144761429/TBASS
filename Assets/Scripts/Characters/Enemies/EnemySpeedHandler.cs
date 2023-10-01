using StackableElement;
using UnityEngine;

public enum EEnemySpeedElementType
{
    Patrol,
    Chase
}

public class EnemySpeedHandler : MonoBehaviour
{
    public float Speed
    {
        get => _speedHandler.CalculateValue();
    }

    private SpeedHandler<EEnemySpeedElementType> _speedHandler;
    [SerializeField] private EnemyDataSO _data;

    private void Awake()
    {
        _speedHandler = new SpeedHandler<EEnemySpeedElementType>();

        _speedHandler.Add(EEnemySpeedElementType.Patrol,
            new StackableElement<float>(_data.PatrolSpeed, new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
                new BoolWrapper(true)));
        _speedHandler.Add(EEnemySpeedElementType.Chase, new StackableElement<float>(_data.PatrolSpeed,
            new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
            new BoolWrapper(true)));
    }

    public StackableElement<float> GetSpeedElement(EEnemySpeedElementType type)
    {
        return _speedHandler.Get(type);
    }
}