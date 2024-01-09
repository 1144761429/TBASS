namespace Characters.Enemies.SerializableData
{
    public class PatrolRioterData : EnemyData
    {
        public float AlertDistance;
        public float WalkSpeed;
        public float SprintSpeed;

        public float PatrolRestTime;
        public float PatrolSpeed;

        public float NormalAttackDamage;
        public float NormalAttackDistance;
        public float NormalAttackCooldown;
        public float NormalAttackAnticipationTime;
        public float NormalAttackRecoveryTime;

        public override string ToString()
        {
            return base.ToString()
                   + $"NormalAttackDamage: {NormalAttackDamage}\n"
                   + $"NormalAttackDistance: {NormalAttackDistance}\n"
                   + $"AlertDistance: {AlertDistance}\n"
                   + $"WalkSpeed: {WalkSpeed}\n"
                   + $"SprintSpeed: {SprintSpeed}\n"
                   + $"PatrolRestTime: {PatrolRestTime}\n"
                   + $"PatrolSpeed: {PatrolSpeed}\n"
                   + $"NormalAttackCooldown: {NormalAttackCooldown}\n"
                   + $"NormalAttackAnticipationTime: {NormalAttackAnticipationTime}\n"
                   + $"NormalAttackRecoveryTime: {NormalAttackRecoveryTime}\n";
        }
    }
}