using UnityEngine;

namespace Characters.Player.Data
{
    public class RuntimePlayerData
    {
        public float CurrentHealth { get; private set; }
        public float CurrentArmor { get; private set; }
        public float WalkSpeedBase { get; private set; }
        public float SprintSpeedBase { get; private set; }
        private PlayerDataSO _staticData;
        
        public RuntimePlayerData(PlayerDataSO staticData)
        {
            _staticData = staticData;
            CurrentHealth = staticData.MaxHP;
            CurrentArmor = staticData.MaxArmor;
            WalkSpeedBase = staticData.WalkSpeed;
            SprintSpeedBase = staticData.SprintSpeed;
        }

        public void AddHealth(float amount)
        {
            CurrentHealth += amount;
        }

        public void ReduceHealth(float amount)
        {
            CurrentHealth -= amount;
        }

        public void AddArmor(float amount)
        {
            CurrentArmor += amount;
        }

        public void ReduceArmor(float amount)
        {
            CurrentArmor -= amount;
        } 
    }
}