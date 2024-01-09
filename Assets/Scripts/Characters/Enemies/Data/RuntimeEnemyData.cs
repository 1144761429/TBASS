using Characters.Enemies.SerializableData;

namespace Characters.Enemies.Data
{
    public class RuntimeEnemyData
    {
        public float MaxHP { get; protected set; }
        public float MinHP { get; protected set; }
        public float CurrentHealth { get; protected set; }
        
        public RuntimeEnemyData(EnemyData staticData)
        {
            MaxHP = staticData.MaxHP;
            MinHP = staticData.MinHP;
            
            CurrentHealth = MaxHP;
        }
        
        public void AddHealth(float amount)
        {
            CurrentHealth += amount;
        }

        public void ReduceHealth(float amount)
        {
            CurrentHealth -= amount;
        }
    }
}