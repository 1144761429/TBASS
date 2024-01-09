using UnityEngine;

namespace BehaviorDesignerCustom.Enemy
{
    public class EnemyNormalAttack : EnemyAction
    {
        public override void OnStart()
        {
            Debug.Log("Enemy Attacking.");
        }
    }
}