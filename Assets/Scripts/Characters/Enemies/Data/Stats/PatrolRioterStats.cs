using Characters.Enemies.Data.RuntimeData;
using Characters.Enemies.SerializableData;

namespace Characters.Enemies.Data.Stats
{
    public class PatrolRioterStats : EnemyStats
    {
        public override EnemyData StaticData { get; protected set; }

        public EnemySpeedHandler SpeedHandler { get; private set; }
        private PatrolRioterRuntimeData _runtimeData => RuntimeData as PatrolRioterRuntimeData;

        protected void Awake()
        {
            StaticData = DatabaseUtil.Instance.GetEnemyData(ID);
            SpeedHandler = new EnemySpeedHandler(_enemy, StaticData as PatrolRioterData);
        }

        public void SetLastAttackTime(float time)
        {
            _runtimeData.SetLastAttackTime(time);
        }
    }
}