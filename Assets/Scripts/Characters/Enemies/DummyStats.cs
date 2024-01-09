using Characters.Enemies.SerializableData;

namespace Characters.Enemies
{
    public class DummyStats : EnemyStats
    {
        public override EnemyData StaticData { get; protected set; }
        
        public float MaxHealth;
        
        public float CurrentHealth { get; private set; }

        protected override void InitializeData()
        {
            StaticData = DatabaseUtil.Instance.GetEnemyData(ID);
            RuntimeData = new PatrolRioterRuntime(StaticData);
        }
    }
}
