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
        
        public bool IsDiscreteHP; // TODO: should this be in runtime data?
        

        public override string ToString()
        {
            return base.ToString()
                   + $"ID: {ID}\n"
                   + $"Name: {Name}\n"
                   + $"MaxHP: {MaxHP}\n"
                   + $"MinHP: {MinHP}\n"
                   + $"IsDiscreteHP: {IsDiscreteHP}\n";
        }
    }
}
