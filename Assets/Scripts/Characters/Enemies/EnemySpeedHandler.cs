using Characters.Enemies.SerializableData;
using StackableElement;

public enum EEnemySpeedElementType
{
    Walk,
    Sprint
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

        public EnemySpeedHandler(Enemy entity, PatrolRioterData data)
        {
            _entity = entity;
            _speedHandler = new SpeedHandler<EEnemySpeedElementType>();
            
            _speedHandler.Add(EEnemySpeedElementType.Walk,
                new StackableElement<float>(data.WalkSpeed, new IntWrapper(1), new IntWrapper(0), new IntWrapper(0),
                    new BoolWrapper(true)));
            _speedHandler.Add(EEnemySpeedElementType.Sprint, new StackableElement<float>(data.SprintSpeed,
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