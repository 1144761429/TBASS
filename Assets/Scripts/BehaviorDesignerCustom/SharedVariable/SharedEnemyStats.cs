using BehaviorDesigner.Runtime;
using Characters.Enemies;
using UnityEngine;

namespace BehaviorDesignerCustom
{
    [System.Serializable]
    public class SharedEnemyStats: SharedVariable<EnemyStats>
    {
        public static implicit operator SharedEnemyStats(EnemyStats value) { return new SharedEnemyStats { Value = value }; }
        
    }
}