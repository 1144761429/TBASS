using Characters.Enemies.SerializableData;

namespace Characters.Enemies.Data.RuntimeData
{
    public class PatrolRioterRuntimeData : RuntimeEnemyData
    {
        public float AlertDistance { get; private set; }
        
        public float WalkSpeed { get; private set; }
        public float SprintSpeed { get; private set; }

        public float PatrolRestTime { get; private set; }
        public float PatrolSpeed { get; private set; }

        public float NormalAttackDamage { get; private set; }
        public float NormalAttackDistance { get; private set; }
        public float NormalAttackCooldown { get; private set; }
        public float NormalAttackAnticipationTime { get; private set; }
        public float NormalAttackRecoveryTime { get; private set; }
        public float LastAttackTime { get; private set; }

        public PatrolRioterRuntimeData(PatrolRioterData staticData) : base(staticData)
        {
            AlertDistance = staticData.AlertDistance;
            
            WalkSpeed = staticData.WalkSpeed;
            SprintSpeed = staticData.SprintSpeed;

            PatrolRestTime = staticData.PatrolRestTime;
            PatrolSpeed = staticData.PatrolSpeed;

            NormalAttackDamage = staticData.NormalAttackDamage;
            NormalAttackDistance = staticData.NormalAttackDistance;
            NormalAttackCooldown = staticData.NormalAttackCooldown;
            NormalAttackAnticipationTime = staticData.NormalAttackAnticipationTime;
            NormalAttackRecoveryTime = staticData.NormalAttackRecoveryTime;
            LastAttackTime = 0;
        }

        public void SetLastAttackTime(float time)
        {
            LastAttackTime = time;
        }
    }
}