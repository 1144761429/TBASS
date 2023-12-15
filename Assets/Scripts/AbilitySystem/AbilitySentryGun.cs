using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Pool;

namespace AbilitySystem
{
    public class AbilitySentryGun : Ability
    {
        public float Duration { get; set; }
        public int MaximumSentryGunOnField { get; set; }
        public int CurrentSentryGunOnField { get; set; }
        public List<SentryGun> SentryGunsOnField { get; set; }
        
        public ObjectPool<GameObject> ProjectilePool { get; private set; }
        
        private GameObject _sentryGunPrefab;
        private GameObject _sentryGunGameObject;
        private SentryGun _sentryGun;
        
        private ContactFilter2D _contactFilter2D;
        private List<Collider2D> _deploymentColliders;

        
        public AbilitySentryGun(IAbilityCaster caster, float duration) : base(caster)
        {
            Duration = duration;
            MaximumSentryGunOnField = 1;
            CurrentSentryGunOnField = 0;
            SentryGunsOnField = new List<SentryGun>();
            //ProjectilePool = new ObjectPool<GameObject>();
            
            _sentryGunPrefab = Resources.Load<GameObject>("Prefabs/Ability/Turret");
            _contactFilter2D = new ContactFilter2D();
            _contactFilter2D.SetLayerMask(LayerMask.GetMask("Obstacle"));
            _deploymentColliders = new List<Collider2D>();
            
            CastRestrictions += IsAreaDeployable;
            CastRestrictions += HasReachMaxOnField;
            OnCast += Deploy;
        }

        private bool IsAreaDeployable()
        {
            //TODO: Change the Vector.right to where the player is facing OR change to deploy sentry by mouse click
            
            if (Physics2D.OverlapBox((Vector2)Caster.CasterTransform.position + Vector2.right, new Vector2(0.5f, 0.5f),
                    0, _contactFilter2D, _deploymentColliders) > 0)
            {
                Debug.Log("Area is occupied by obstacles. Cannot deploy sentry gun.");
                return false;
            }

            return true;
        }

        private bool HasReachMaxOnField()
        {
            return CurrentSentryGunOnField + 1 <= MaximumSentryGunOnField;
        }

        private void Deploy()
        {
            _sentryGunGameObject = Object.Instantiate(_sentryGunPrefab, (Vector2)Caster.CasterTransform.position + Vector2.right,
                Quaternion.identity);
            _sentryGun = _sentryGunGameObject.GetComponent<SentryGun>();
            
            CurrentSentryGunOnField++;
            _sentryGun.Init(this, Caster);
            SentryGunsOnField.Add(_sentryGun);
            _sentryGunGameObject.GetComponent<BehaviorTree>().EnableBehavior();
            _sentryGun.StartCountDown(Duration);
        }

        #region Object Pool Methods

        // private GameObject OnCreateProjectile()
        // {
        //     
        // }

        #endregion
    }
}