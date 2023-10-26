using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace WeaponSystem
{
    public enum EProjectilePatternType
    {
        R1P1,
        R1P5
    }
    
    /// <summary>
    /// A class that represents the projectile behavior of a ranged weapon.
    /// </summary>
    ///
    /// <example>
    /// <para>AR's projectile behavior: 1 rounds of projectile with 1 projectile.</para>
    /// <para>SMG's projectile behavior: 1 rounds of projectile with 1 projectile.</para>
    /// <para>Shotgun's projectile behavior: 1 rounds of projectile with multiple projectiles.</para>
    /// <para>Gun with burst fire mode's projectile behavior: 3 rounds of projectile with 1 projectile.</para>
    /// </example>
    [CreateAssetMenu(fileName  = "NewProjectilePattern", menuName = "WeaponSystem/Projectile Pattern")]
    public class ProjectilePattern : ScriptableObject
    {
        public List<SingleRoundProjectilePattern> RoundsOfProjectile;

        public int TotalProjectilesNum()
        {
            int amount = 0;
            
            foreach (var round in RoundsOfProjectile)
            {
                amount += round.Projectiles.Count;
            }

            return amount;
        }
        
        public void SetSpeed(int roundNum, int projectileNum, float speed)
        {
            roundNum--;
            projectileNum--;
            
            if (roundNum < 0 || projectileNum < 0 || roundNum >= RoundsOfProjectile.Count
                || projectileNum >= RoundsOfProjectile[roundNum].Projectiles.Count)
            {
                throw new ArgumentOutOfRangeException(
                    message: $"The projectile pattern has {RoundsOfProjectile.Count} rounds " +
                             $"and the rounds have {ProjectileInEachRoundToString()} projectile(s) in each. " +
                             $"But the argument is roundNum: {roundNum}  projectileNum: {projectileNum}.", null);
            }

            RoundsOfProjectile[roundNum].Projectiles[projectileNum].Speed = speed;
        }
        
        public void SetSpeed(List<List<float>> speed)
        {
            if (!CheckSizeEquality(speed))
            {
                throw new ArgumentOutOfRangeException(
                    message: $"The projectile pattern has {RoundsOfProjectile.Count} rounds " +
                             $"and the rounds have {ProjectileInEachRoundToString()} projectile(s) in each. " +
                             $"But the argument list has {speed.Count} rounds and {ProjectileInEachRoundToString(speed)} projectile in each round.",
                    null);
            }

            for (int round = 0; round < RoundsOfProjectile.Count; round++)
            {
                for (int projectileIndex = 0; projectileIndex < RoundsOfProjectile[round].Projectiles.Count; projectileIndex++)
                {
                    RoundsOfProjectile[round].Projectiles[projectileIndex].Speed = speed[round][projectileIndex];
                }
            }
        }
        
        public void SetAngleOffset(int roundNum, int projectileNum, float angleOffset)
        {
            roundNum--;
            projectileNum--;
            
            if (roundNum < 0 || projectileNum < 0 || roundNum >= RoundsOfProjectile.Count
                || projectileNum >= RoundsOfProjectile[roundNum].Projectiles.Count)
            {
                throw new ArgumentOutOfRangeException(
                    message: $"The projectile pattern has {RoundsOfProjectile.Count} rounds " +
                             $"and the rounds have {ProjectileInEachRoundToString()} projectile(s) in each. " +
                             $"But the argument is roundNum: {roundNum}  projectileNum: {projectileNum}.", null);
            }

            RoundsOfProjectile[roundNum].Projectiles[projectileNum].AngleOffset = angleOffset;
        }

        public void SetAngleOffset(List<List<float>> angleOffset)
        {
            if (!CheckSizeEquality(angleOffset))
            {
                throw new ArgumentOutOfRangeException(
                    message: $"The projectile pattern has {RoundsOfProjectile.Count} rounds " +
                             $"and the rounds have {ProjectileInEachRoundToString()} projectile(s) in each. " +
                             $"But the argument list has {angleOffset.Count} rounds and {ProjectileInEachRoundToString(angleOffset)} projectile in each round.",
                    null);
            }

            for (int round = 0; round < RoundsOfProjectile.Count; round++)
            {
                for (int projectileIndex = 0; projectileIndex < RoundsOfProjectile[round].Projectiles.Count; projectileIndex++)
                {
                    RoundsOfProjectile[round].Projectiles[projectileIndex].AngleOffset = angleOffset[round][projectileIndex];
                }
            }
        }
        
        private string ProjectileInEachRoundToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.Append("[ ");
            
            foreach (SingleRoundProjectilePattern round in RoundsOfProjectile)
            {
                stringBuilder.Append($"{round.Projectiles.Count} ");
            }
            stringBuilder.Append(" ]");

            return stringBuilder.ToString();
        }
        
        private string ProjectileInEachRoundToString(List<List<float>> lists)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.Append("[ ");
            
            foreach (List<float> list in lists)
            {
                stringBuilder.Append($"{list.Count} ");
            }
            stringBuilder.Append(" ]");

            return stringBuilder.ToString();
        }

        private bool CheckSizeEquality(List<List<float>> lists)
        {
            //Debug.Log(lists.Count + "  " + ProjectileInEachRoundToString(lists));
            
            if (lists.Count != RoundsOfProjectile.Count)
            {
                Debug.Log("Not the same rounds num");
                return false;
            }
            
            int index = 0;
            
            foreach (List<float> sublist in lists)
            {
                if (sublist.Count != RoundsOfProjectile[index].Projectiles.Count)
                {
                    Debug.Log("Not the same projectile num at round " + (index + 1));
                    return false;
                }

                index++;
            }

            //Debug.Log("Equals");
            return true;
        }
    }

    [Serializable]
    public class SingleRoundProjectilePattern
    {
        public float IntervalBetweenPreviousRound;
        public List<SingleProjectilePattern> Projectiles;
    }
    
    [Serializable]
    public class SingleProjectilePattern
    {
        public float AngleOffset;
        public float Speed;
    }
}