using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponSystem.DamagingEntities;
using Object = UnityEngine.Object;

namespace WeaponSystem
{
    public class ShootingModule : WeaponModule
    {
        public override EWeaponModule ModuleType => EWeaponModule.ShootingModule;

        #region Module Events
        
        public event Func<bool> ShootTrigger; // This will be the MainFuncTrigger or AltFuncTrigger
        
        public event Action BeforeCheckShootCondition;
        public event Func<bool> ShootCondition;
        public event Func<bool> SecondaryShootCondition;
        
        public event Action BeforeShoot;
        public event Action OnShoot;
        public event Action AfterShoot;

        public event Action BeforeShootConditionFail;
        public event Action OnShootConditionFail;
        public event Action AfterShootConditionFail;
        #endregion

        /// <summary>
        /// An event that contains what type of shoot behavior a weapon should have.
        /// </summary>
        /// <example>
        /// AR, SMG, Rocket: physical.
        /// Laser: instant register.
        /// </example>
        private event Func<IEnumerator> _shootBehavior;

        private ProjectilePattern _projectilePattern;
        
        /// <summary>
        /// The last time this weapon is fired.
        /// </summary>
        private float _lastFireTime;
        private Transform _bulletSpawn => _weapon.BulletSpawnPos;
        private LineRenderer _lineRenderer;

        public ShootingModule(Weapon weapon, ItemDataEquipmentWeapon staticData,
            RuntimeItemDataEquipmentWeapon runtimeData, EProjectilePatternType projectilePattern) : base(weapon,
            weapon, staticData, runtimeData)
        {
            InitializeProjectilePattern(projectilePattern);
            
            // Set the fire mode according to the Static Data
            // TODO: what if a weapon can switch fire mode in game?
            switch (_staticData.FireMode)
            {
                case EFireMode.Single:
                    ShootTrigger = () => WeaponInputHandler.Instance.MainFunctionKeyPressed;
                    break;
                case EFireMode.Auto:
                    ShootTrigger = () => WeaponInputHandler.Instance.MainFunctionKeyHeld;
                    break;
            }

            // Set the shoot behavior according to if this gun shoots physic projectile or instant registered projectile like lasers.
            switch (_staticData.BulletType)
            {
                case EBulletType.Physic:
                    _shootBehavior += PhysicShoot;
                    break;
                case EBulletType.InstantRegister:
                    InitLineRendererForInstRegShootBehavior();
                    _shootBehavior += InstantRegisterShoot;
                    break;
            }

            ShootCondition += ShootCooldownPassed;
            ShootCondition += () =>  _dependencyHandler.HaveAmmoInMag;
            ShootCondition += () => !_dependencyHandler.IsReloading;

            BeforeShoot += UpdateLastFireTime;

            OnShootConditionFail += () =>
            {
                Debug.Log("Shoot Condition Fail");
            };
            
            _weapon.Events.MainFuncTriggerCondition += ShootTrigger;

            //TODO: Refactor. Do not involve things related to charge module.
            if (_staticData.HasChargeModule && _weapon.gameObject.TryGetComponent(out ChargeModule chargeModule))
            {
                _weapon.Events.MainFuncCancelCallback += StartShoot;
            }
            else
            {
                _weapon.Events.MainFunc += StartShoot;
            }
        }
        
        /// <summary>
        /// Load the correct ProjectilePattern from prefab according to the static data.
        /// </summary>
        /// <param name="type">The enum that represents the type of ProjectilePattern</param>
        public void InitializeProjectilePattern(EProjectilePatternType type)
        {
            List<List<float>> speedInfo;
            List<List<float>> angleOffsetInfo;
            
            //TODO: replace the number in the {new List<float> { #number }} to a variable in static data
            switch (type)
            {
                case EProjectilePatternType.R1P1:
                    //_projectilePattern = Resources.Load<ProjectilePattern>("Projectile Patterns/R1P1");
                    _projectilePattern =
                        Object.Instantiate(Resources.Load<ProjectilePattern>("Projectile Patterns/R1P1"));
                    speedInfo = new List<List<float>> { new List<float> { 5 } };
                    angleOffsetInfo = new List<List<float>> { new List<float> { 0 } };
                    _projectilePattern.SetSpeed(speedInfo);
                    _projectilePattern.SetAngleOffset(angleOffsetInfo);
                    break;
                case EProjectilePatternType.R1P5:
                    //_projectilePattern = Resources.Load<ProjectilePattern>("Projectile Patterns/R1P5");
                    _projectilePattern =
                        Object.Instantiate(Resources.Load<ProjectilePattern>("Projectile Patterns/R1P5"));
                    speedInfo = new List<List<float>> { new List<float> { 2, 2, 2, 2, 2 } };
                    angleOffsetInfo = new List<List<float>> { new List<float> { -22.5f, -11.25f, 0, 11.25f, 22.5f } };
                    _projectilePattern.SetSpeed(speedInfo);
                    _projectilePattern.SetAngleOffset(angleOffsetInfo);
                    break;
            }
        }

        private bool ShootCooldownPassed()
        {
            if (_lastFireTime + 60 / _staticData.RPM <= Time.time)
            {
                return true;
            }

            // Debug.Log(
            //     $"Shoot cool down not passed. Current time: {Time.time}. Next fire time: {_lastFireTime + 60 / _staticData.RPM}");
            return false;

        }

        private void StartShoot()
        {
            BeforeCheckShootCondition?.Invoke();

            if (FuncBoolUtil.Evaluate(ShootCondition))
            {
                if (FuncBoolUtil.Evaluate(SecondaryShootCondition))
                {
                    _mono.StartCoroutine(ShootProcedure());
                }
                else
                {
                    OnShootConditionFail?.Invoke();
                } 
            }
        }

        private IEnumerator ShootProcedure()
        {
            BeforeShoot?.Invoke();
            yield return _shootBehavior();
            OnShoot?.Invoke();
            AfterShoot?.Invoke();
            _runtimeData.AimAngle = _staticData.DefaultAimAngle;
        }

        private IEnumerator PhysicShoot()
        {
            int initProjectileCount = 0;
            int shotProjectileCount = 0;

            // Round
            for (int round = 0; round < _projectilePattern.RoundsOfProjectile.Count; round++)
            {
                Bullet[] projectiles = _dependencyHandler.GetBullet(_projectilePattern.TotalProjectilesNum());
                
                // Projectiles in a round
                for (int projectileIndex = 0;
                     projectileIndex < _projectilePattern.RoundsOfProjectile[round].Projectiles.Count;
                     projectileIndex++, initProjectileCount++)
                {
                    //Debug.Log(_projectilePattern.RoundsOfProjectile[round].Projectiles.Count);
                    Bullet currentProjectile = projectiles[initProjectileCount];
                    Transform currentProjectileTransform = currentProjectile.transform;
                    
                    float staticSpeed = _projectilePattern.RoundsOfProjectile[round].Projectiles[projectileIndex].Speed;
                    float staticAngleOffset = _projectilePattern.RoundsOfProjectile[round].Projectiles[projectileIndex].AngleOffset;
                    
                    Vector2 vector2FromWeaponToMouse = MouseUtil.GetVector2ToMouse(_weapon.BulletSpawnPos.position);
                    
                    // Actual shoot angle = shoot angle (where the mouse is pointing) + angle offset (the offset according to static data, or projectile pattern)
                    float actualProjectileAngle = Vector2.SignedAngle(Vector2.right, vector2FromWeaponToMouse);
                    actualProjectileAngle += vector2FromWeaponToMouse.x > 0 ? staticAngleOffset : -staticAngleOffset;
                    
                    // Convert angles to normalized vector  
                    float velocityX = MathF.Cos(actualProjectileAngle * Mathf.Deg2Rad);
                    float velocityY = MathF.Sin(actualProjectileAngle * Mathf.Deg2Rad);
                    Vector2 finalVelocity = new Vector2(velocityX, velocityY).normalized;
                    
                    // Set the rotation of the graphic of the current projectile.
                    currentProjectileTransform.localRotation = Quaternion.Euler(0, 0, actualProjectileAngle);
                    
                    // Set the initial position of the current projectile to Bullet Spawn.
                    currentProjectileTransform.position = _bulletSpawn.transform.position;
                    
                    currentProjectile.Rb.velocity = finalVelocity.normalized * staticSpeed;

                    // Vector3 tempV3 = new Vector3(_weapon.BulletSpawnPos.transform.position.x,
                    //     _weapon.BulletSpawnPos.transform.position.y, 0);
                    // Debug.DrawLine(tempV3,finalVelocity.normalized*10f);
                }
                
                while (shotProjectileCount < initProjectileCount)
                {
                    projectiles[shotProjectileCount].Traversing = true;
                    shotProjectileCount++;
                }
                
                yield return new WaitForSecondsRealtime(_projectilePattern.RoundsOfProjectile[round].IntervalBetweenPreviousRound);
            }
        }

        private IEnumerator InstantRegisterShoot()
        {
            for (var i = 0; i < _staticData.BulletPerFire; i++)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, _weapon.transform.position);
                Vector2 destination = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _lineRenderer.SetPosition(1, destination);
                yield return new WaitForSecondsRealtime(0.5f);
                _lineRenderer.enabled = false;
            }

            _runtimeData.ReduceAmmo(1);
        }

        private void UpdateLastFireTime()
        {
            _lastFireTime = Time.time;
        }

        /// <summary>
        /// Add and set up a Line Renderer component for the weapon for instant register shoot behavior weapon.
        /// </summary>
        private void InitLineRendererForInstRegShootBehavior()
        {
            GameObject lineRendererPrefab =
                Object.Instantiate(Resources.Load<GameObject>("Prefabs/WeaponComponent/InstantRegisterLaser"),
                    _weapon.transform);
            _lineRenderer = lineRendererPrefab.GetComponent<LineRenderer>();
            _lineRenderer.material = Resources.Load<Material>(_staticData.BulletAssetPath);
            _lineRenderer.enabled = false;
        }
    }
}