using System;

namespace Characters.Enemies.SerializableData
{
    [Serializable]
    public class EnemyData
    {
        public int ID;
        public String Name;
        
        public float MaxHP;
        public float MinHP;
        public bool IsDiscreteHP;
        
        public float AttackDamage;
        public float AttackRange;

        public float AlertDistance;
        public float MoveSpeed;
        public float ChaseSpeed;
        
        public float PatrolIdleTime;
        public float PatrolSpeed;
        
        
        public override string ToString()
        {
            return base.ToString()
                   + $"ID: {ID}\n"
                   + $"Name: {Name}\n"
                   + $"MaxHP: {MaxHP}\n"
                   + $"MinHP: {MinHP}\n"
                   + $"IsDiscreteHP: {IsDiscreteHP}\n"
                   + $"AttackDamage: {AttackDamage}\n"
                   + $"AttackRange: {AttackRange}\n"
                   + $"AlertDistance: {AlertDistance}\n"
                   + $"MoveSpeed: {MoveSpeed}\n"
                   + $"ChaseSpeed: {ChaseSpeed}\n"
                   + $"PatrolIdleTime: {PatrolIdleTime}\n"
                   + $"PatrolSpeed: {PatrolSpeed}\n";
        }
    }
    
}