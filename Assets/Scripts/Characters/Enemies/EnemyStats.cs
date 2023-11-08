using System;
using Characters.Enemies.SerializableData;

namespace Characters.Enemies
{
    /// <summary>
    /// A class that stores the static data and runtime data of an enemy.
    /// </summary>
    public class EnemyStats
    {
        private Enemy _enemy;
        public EnemyData StaticData { get; private set; }

        #region Runtime Data
        
        public float CurrentHP { get; private set; }
        public int CurrentWayPointIndex { get; private set; }
        public float LastAttackTime { get; private set; }
        public EnemySpeedHandler SpeedHandler { get; private set; }
        
        
        #endregion

        
        public EnemyStats(Enemy enemy, int id)
        {
            _enemy = enemy;
            
            StaticData = DatabaseUtil.Instance.GetEnemyData(id);

            CurrentHP = StaticData.MaxHP;
            CurrentWayPointIndex = 0;
            LastAttackTime = 0;
            
            SpeedHandler = new EnemySpeedHandler(_enemy, StaticData);
        }

        public void SetCurrentHP(float newCurrentHP)
        {
            CurrentHP = newCurrentHP;
        }

        public void SetCurrentWayPointIndex(int idx)
        {
            if (idx >= _enemy.PatrolWayPoints.Count || idx < 0)
            {
                throw new ArgumentException(
                    $"Invalid way point index. Argument: {idx}, Range: 0 to {_enemy.PatrolWayPoints.Count - 1} inclusive.");
            }

            CurrentWayPointIndex = idx;
        }

        public void SetLastAttackTime(float time)
        {
            LastAttackTime = time;
        }
    }
}