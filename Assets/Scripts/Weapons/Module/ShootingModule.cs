using System;
using System.Collections;
using UnityEngine;

namespace WeaponSystem
{
    public class ShootingModule : WeaponModule
    {
        public Func<bool> ShootTrigger;
        public Func<bool> CanShootCondition;
        public Action ActionBeforeShoot;
        public Func<bool> CanEnterOnShootCondition;
        public Action ActionOnShoot;
        public Action ActionAfterShoot;

        private Func<IEnumerator> Shoot;

        private AmmunitionModule _ammunitionModule;
        private Transform _bulletSpawn;
        private float _lastFireTime;
        private LineRenderer _lineRenderer;


        protected override void Awake()
        {
            base.Awake();

            if (!TryGetComponent(out _ammunitionModule))
                throw new Exception("Ammunition Module is required to properly use Shooting Module");

            _bulletSpawn = _weapon.BulletSpawn;
        }

        public override void Init()
        {
            base.Init();

            switch (_data.FireMode)
            {
                case EFireMode.Single:
                    ShootTrigger = SingleShootModeTrigger;
                    break;
                case EFireMode.Auto:
                    ShootTrigger = AutoShootModeTrigger;
                    break;
            }

            switch (_data.BulletType)
            {
                case EBulletType.Physic:
                    Shoot += PhysicShoot;
                    break;
                case EBulletType.InstantRegister:
                    GameObject lineRendererPrefab =
                        Instantiate(Resources.Load<GameObject>("Prefabs/WeaponComponent/InstantRegisterLaser"),
                            transform);
                    _lineRenderer = lineRendererPrefab.GetComponent<LineRenderer>();
                    _lineRenderer.material = Resources.Load<Material>(_data.BulletAssetPath);
                    _lineRenderer.enabled = false;
                    Shoot += InstantRegisterShoot;
                    break;
            }

            CanShootCondition += ShootCooldownPassed;
            CanShootCondition += _ammunitionModule.HaveAmmoInMag;
            CanShootCondition += _ammunitionModule.IsNotReloading;

            ActionBeforeShoot += UpdateLastFireTime;

            _weapon.MainFuncTrigger += ShootTrigger;

            if (_data.HasChargeModule)
            {
                ChargeModule chargeModule = GetComponent<ChargeModule>();
                _weapon.MainFuncCancelCondition += () => _weapon.Loadout.InputHandler.MainFunctionKeyReleased;
                _weapon.OnCancelMainFunc += StartShoot;
                _weapon.OnCancelMainFunc += () =>
                {
                    if (chargeModule.ChargeProgress < chargeModule.ChargeThreshold)
                    {
                        StopAllCoroutines();
                    }
                };
            }
            else
            {
                _weapon.MainFunction += StartShoot;
            }
        }

        private bool AutoShootModeTrigger()
        {
            return _weapon.Loadout.InputHandler.MainFunctionKeyHeld;
        }

        private bool SingleShootModeTrigger()
        {
            return _weapon.Loadout.InputHandler.MainFunctionKeyPressed;
        }

        private bool ShootCooldownPassed()
        {
            return _lastFireTime + 60 / _data.RPM <= Time.time;
        }

        private void StartShoot()
        {
            if (FuncBoolUtil.Evaluate(CanShootCondition))
                StartCoroutine(ShootProcess());
            else
                print("Cannot Start shooting");
        }

        private IEnumerator ShootProcess()
        {
            ActionBeforeShoot?.Invoke();
            if (CanEnterOnShootCondition != null)
                yield return new WaitUntil(CanEnterOnShootCondition);
            yield return Shoot();
            ActionOnShoot?.Invoke();
            ActionAfterShoot?.Invoke();
            _runtimeData.AimAngle = _data.DefaultAimAngle;
        }

        private IEnumerator PhysicShoot()
        {
            var bullets = _ammunitionModule.GetBullet(_data.BulletPerFire);

            for (var i = 0; i < _data.BulletPerFire; i++)
            {
                var bullet = bullets[i];

                // If there is no more available ammo in mag
                if (bullet == null)
                {
                    Debug.LogWarning($"The {i}th Bullet of this shot is null. Please verify if it is an error.");
                    continue;
                }

                bullet.transform.position = _bulletSpawn.transform.position;
                bullet.transform.rotation = Quaternion.Euler(0, 0,
                    Vector2.SignedAngle(Vector2.right, MouseUtil.GetVector2ToMouse(transform.position)));

                var defaultShootDirection = MouseUtil.GetVector2ToMouse(transform.position).normalized;
                var defaultShootAngle = -Vector2.SignedAngle(Vector2.right, defaultShootDirection);

                float finalShootAngle;
                //The angle offset from the mouse direction
                var angleOffset = _data.BulletSpreadAngles[i] *
                                  (MouseUtil.GetVector2ToMouse(transform.position).x > 0 ? 1 : -1) *
                                  (_runtimeData.AimAngle / _data.DefaultAimAngle);

                //*****************************************************REFACTOR*****************************************************
                if (0 <= defaultShootAngle && defaultShootAngle <= 180 && defaultShootAngle + angleOffset > 180)
                    finalShootAngle = defaultShootAngle + angleOffset - 360;
                else if (0 >= defaultShootAngle && defaultShootAngle >= -180 && defaultShootAngle + angleOffset < -180)
                    finalShootAngle = defaultShootAngle + angleOffset + 360;
                else
                    finalShootAngle = defaultShootAngle + angleOffset;
                //*****************************************************REFACTOR*****************************************************

                var finalShootDirX = MathF.Cos(finalShootAngle * Mathf.Deg2Rad);
                var finalShootDirY = MathF.Sin(finalShootAngle * Mathf.Deg2Rad);
                var finalShootDir = new Vector2(finalShootDirX, -finalShootDirY).normalized;
                bullet.transform.localEulerAngles += new Vector3(0, 0, -_data.BulletSpreadAngles[i]) *
                                                     (MouseUtil.GetVector2ToMouse(transform.position).x > 0 ? -1 : 1);
                bullet.RB.velocity += finalShootDir * _data.BulletSpeed;
                bullet.RB.velocity = (MouseUtil.GetVector2ToMouse(transform.position) + finalShootDir).normalized *
                                     _data.BulletSpeed;

                yield return new WaitForSecondsRealtime(_data.ShotTimeInterval);
            }
        }

        private IEnumerator InstantRegisterShoot()
        {
            for (var i = 0; i < _data.BulletPerFire; i++)
            {
                _lineRenderer.enabled = true;
                _lineRenderer.SetPosition(0, transform.position);
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

        // private void ReduceAmmoInMag()
        // {
        //     _runtimeData.ammoInMag -= 1;
        // }
    }
}