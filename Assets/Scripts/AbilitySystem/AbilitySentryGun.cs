using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;
using UnityEngine.Pool;
using WeaponSystem;
using WeaponSystem.DamagingEntities;

namespace AbilitySystem
{
    [CreateAssetMenu(fileName = "NewSentryGun", menuName = "Ability/SentryGun")]
    public class AbilitySentryGun : Ability
    {
        [Tooltip("Prefab of the sentry gun.")]
        [field: SerializeField]
        public GameObject SentryGunPrefab { get; set; }

        [Tooltip("The projectile that the sentry gun shoots.")]
        [field: SerializeField]
        public Projectile Projectile { get; set; }

        [Tooltip("The projectile pattern of how the sentry gun shoots.")]
        [field: SerializeField]
        public ProjectilePattern ProjectilePattern { get; set; }

        [Tooltip("The damage of each shot of the sentry gun.")]
        [field: SerializeField]
        public float Damage { get; set; }

        [Tooltip("The duration of how long the sentry gun exists on the field.")]
        [field: SerializeField]
        public float Duration { get; set; }

        [Tooltip("The speed of how fast the projectile travels.")]
        [field: SerializeField]
        public float ProjectileTravelSpeed { get; set; }

        [Tooltip("The delay between each time the sentry gun attempts to shoot.")]
        [field: SerializeField]
        public float PreparationTimeForShoot { get; set; }

        [Tooltip("The maximum amount of sentry gun that can exist on field.")]
        [field: SerializeField]
        public int MaximumSentryGunOnField { get; set; }

        /// <summary>
        /// Amount of sentry guns that currently on the field.
        /// </summary>
        public int SentryGunOnFieldCurrentNumber { get; set; }
        
        /// <summary>
        /// A list of sentry guns that are currently on the field.
        /// </summary>
        public List<SentryGun> SentryGunsOnField { get; set; }
        
        [field: SerializeField] public ContactFilter2D ContactFilter { get; private set; }
        
        /// <summary>
        /// The object pool that handles the projectile entity of the sentry gun.
        /// </summary>
        public ObjectPool<Projectile> ProjectilePool { get; private set; }
        
        /// <summary>
        /// A list that stores the collider when in the deployment area.
        /// </summary>
        private List<Collider2D> _undeployableColliders;

        /// <summary>
        /// A method that initialize the ability.
        /// </summary>
        /// <param name="caster">The caster of this ability.</param>
        public void Init(IAbilityCaster caster)
        {
            base.Init(caster);

            SentryGunsOnField = new List<SentryGun>();

            ProjectilePool = new ObjectPool<Projectile>(CreateProjectile, GetProjectile, ReleaseProjectile,
                DestroyProjectile,
                false, 30, 50);

            _undeployableColliders = new List<Collider2D>();

            CastRestrictions += IsAreaDeployable;
            CastRestrictions += HasReachMaxOnField;
            OnCast += Deploy;
        }

        /// <summary>
        /// Get projectiles from the projectile object pool.
        /// </summary>
        /// <param name="amount">The number of projectiles to get.</param>
        /// <param name="sentryGun">The sentry gun that is asking for projectiles.</param>
        /// <returns></returns>
        public Projectile[] GetProjectile(int amount, SentryGun sentryGun)
        {
            Projectile[] projectiles = new Projectile[amount];

            for (int i = 0; i < amount; i++)
            {
                projectiles[i] = ProjectilePool.Get();
                projectiles[i].Init(sentryGun.gameObject, ProjectilePool, true);
            }

            return projectiles;
        }
        
        // TODO: Change the deployable area check based on which direction the player is facing.
        /// <summary>
        /// Check if the area is deployable.
        /// </summary>
        /// <returns>True if the area is deployable. False if the area is NOT deployable.</returns>
        private bool IsAreaDeployable()
        {
            //TODO: Change the Vector.right to where the player is facing OR change to deploy sentry by mouse click

            if (Physics2D.OverlapBox((Vector2)Caster.CasterTransform.position + Vector2.right, new Vector2(0.5f, 0.5f),
                    0, ContactFilter, _undeployableColliders) > 0)
            {
                Debug.Log("Area is occupied by obstacles. Cannot deploy sentry gun.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the sentry guns on the field has reached the maximum.
        /// </summary>
        /// <returns>True if the number has reached maximum. False if the number has NOT reach maximum.</returns>
        private bool HasReachMaxOnField()
        {
            return SentryGunOnFieldCurrentNumber + 1 <= MaximumSentryGunOnField;
        }

        // TODO: change the deploy position based on where the playing is facing.
        /// <summary>
        /// Deploy the sentry gun.
        /// </summary>
        private void Deploy()
        {
            GameObject sentryGunGameObject = Object.Instantiate(SentryGunPrefab,
                (Vector2)Caster.CasterTransform.position + Vector2.right,
                Quaternion.identity);
            SentryGun sentryGun = sentryGunGameObject.GetComponent<SentryGun>();

            SentryGunOnFieldCurrentNumber++;
            sentryGun.Init(this, Caster);
            SentryGunsOnField.Add(sentryGun);
            sentryGun.BehaviorTree.EnableBehavior();
            sentryGun.StartCountDown(Duration);
        }

        #region Object Pool Methods

        private Projectile CreateProjectile()
        {
            GameObject bulletGameObj = Instantiate(Projectile.gameObject, null, true);

            bulletGameObj.name = $"Sentry Gun Bullet {ProjectilePool.CountAll + 1}";
            Projectile projectile = bulletGameObj.GetComponent<Projectile>();

            return projectile;
        }

        private void GetProjectile(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
        }

        private void ReleaseProjectile(Projectile projectile)
        {
            projectile.Traversing = false;
            projectile.gameObject.SetActive(false);
            projectile.ResetTraveledDistance();
        }

        private void DestroyProjectile(Projectile projectile)
        {
            Destroy(projectile.gameObject);
        }

        #endregion
    }
}