using System;
using UnityEngine;

namespace BehaviorDesignerCustom
{
    public interface IAttackAction
    {
        Action<GameObject, IDamageable> BeforeAttack { get; }
        Action<GameObject, IDamageable> OnAttack { get; }
        Action<GameObject, IDamageable> AfterAttack { get; }
        void Attack();
    }
}