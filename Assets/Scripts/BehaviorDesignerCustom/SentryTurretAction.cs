using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace BehaviorDesignerCustom
{
    public abstract class  SentryTurretAction : Action
    {
        protected SentryGun sentryGun;
        protected Rigidbody2D rigidbody;
        protected CircleCollider2D collider;
        // protected Animator

        public override void OnAwake()
        {
            sentryGun = GetComponent<SentryGun>();
            rigidbody = GetComponent<Rigidbody2D>();
            collider = GetComponent<CircleCollider2D>();
        }
    }
}