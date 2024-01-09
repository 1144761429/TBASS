using Characters.Enemies.Data.RuntimeData;
using Characters.Enemies.SerializableData;

namespace Characters.Enemies.Data.Stats
{
    public class PatrolRioterStats : EnemyStats
    {
        public override EnemyData StaticData { get; protected set; }

        public EnemySpeedHandler SpeedHandler { get; private set; }
        private PatrolRioterRuntimeData _runtimeData => RuntimeData as PatrolRioterRuntimeData;

        protected override void InitializeData()
        {
            StaticData = DatabaseUtil.Instance.GetEnemyData(ID);
            RuntimeData = new PatrolRioterRuntime(StaticData);
            
            SpeedHandler = new EnemySpeedHandler(_enemy, StaticData as PatrolRioterData);
        }

        public void SetLastAttackTime(float time)
        {
            _runtimeData.SetLastAttackTime(time);
        }
    }
}
