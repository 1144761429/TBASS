using Characters.Enemies.SerializableData;
using StackableElement;

public enum EEnemySpeedElementType
{
    Patrol,
    Chase
}

namespace Characters.Enemies
{
    public class EnemySpeedHandler
    {
        private Enemy _entity;

        public float Speed
        {
            get => _speedHandler.CalculateValue();
        }

        private SpeedHandler<EEnemySpeedElementType> _speedHandler;
        private EnemyData _data;

        public EnemySpeedHandler(Enemy entity, EnemyData data)
        {
            _entity = entity;
            _speedHandler = new SpeedHandler<EEnemySpeedElementType>();
            
            _speedHandler.Add(EEnemySpeedElementType.Patrol,
                new StackableElement<float>(data.PatrolSpeed, new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
                    new BoolWrapper(true)));
            _speedHandler.Add(EEnemySpeedElementType.Chase, new StackableElement<float>(data.ChaseSpeed,
                new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
                new BoolWrapper(true)));
        }

        public StackableElement<float> GetSpeedElement(EEnemySpeedElementType type)
        {
            return _speedHandler.Get(type);
        }

        public void Debug()
        {
            _speedHandler.DebugContainedStackableElement();
        }
    }
}