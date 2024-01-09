using Characters.Enemies.SerializableData;

namespace Characters.Enemies
{
    public class DummyStats : EnemyStats
    {
        public float MaxHealth;
        
        public float CurrentHealth { get; private set; }

        public override EnemyData StaticData
        {
            get => throw new System.NotImplementedException();
            protected set => throw new System.NotImplementedException();
        }
    }
}