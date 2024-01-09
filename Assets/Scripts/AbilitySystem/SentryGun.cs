using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AbilitySystem;
using BehaviorDesigner.Runtime;
using CombatSystem;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;
using UnityTimer;
using WeaponSystem;
using WeaponSystem.DamagingEntities;

public class SentryGun : MonoBehaviour, IProjectileLauncher
{
    public IAbilityCaster Caster { get; private set; }
    public AbilitySentryGun AbilitySentryGun { get; private set; }

    public HashSet<IDamageable> PossibleTarget { get; private set; }
    public GameObject CurrentTarget { get; private set; }

    public float Duration => _timer.duration;
    public float RemainingDuration => _timer.GetTimeRemaining();
    public float PreparationTime => AbilitySentryGun.PreparationTimeForShoot;
    public BehaviorTree BehaviorTree { get; private set; }

    private ObjectPool<Projectile> _projectilePool;
    private Timer _timer;
    private Collider2D _collider;
    

    private bool _initialized;

    #region MonoBehaviour Methods

    private void Awake()
    {
        PossibleTarget = new HashSet<IDamageable>();

        _collider = GetComponent<Collider2D>();
        BehaviorTree = GetComponent<BehaviorTree>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        AddPossibleTarget(damageable);
    }
    
    private IEnumerator OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            yield break;
        }
    
        AddPossibleTarget(damageable);
    
        yield return new WaitForSeconds(1 / 20f);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        RemovePossibleTarget(damageable);
    }

    #endregion

    /// <summary>
    /// Initialize the sentry gun.
    /// This method must be called before the sentry functions correctly.
    /// </summary>
    /// <param name="abilitySentryGun">The source ability scriptable object.</param>
    /// <param name="caster">The caster of this sentry gun.</param>
    public void Init(AbilitySentryGun abilitySentryGun, IAbilityCaster caster)
    {
        AbilitySentryGun = abilitySentryGun;
        _projectilePool = abilitySentryGun.ProjectilePool;
        Caster = caster;
        _initialized = true;
    }

    /// <summary>
    /// Register a timer that handles the duration of the sentry gun.
    /// </summary>
    /// <param name="time"></param>
    public void StartCountDown(float time)
    {
        if (!_initialized)
        {
            throw new Exception("Sentry is not initialized. Please invoke Init() before calling StartCountDown.");
        }
        
        // When the time completely elapse, invoke relevant event method.
        _timer = Timer.Register(time, OnDurationPassed);
    }

    /// <summary>
    /// Extend the duration of the sentry gun based on the remaining duration.
    /// </summary>
    /// <param name="extendedTime">Time to extend.</param>
    public void ExtendDuration(float extendedTime)
    {
        if (!_initialized)
        {
            throw new Exception("Sentry is not initialized. Please invoke Init() before calling ExtendDuration.");
        }
        
        _timer.Cancel();
        _timer = Timer.Register(_timer.GetTimeRemaining() + extendedTime, null);
    }

    private void AddPossibleTarget(IDamageable damageable)
    {
        if (!_initialized)
        {
            throw new Exception("Sentry is not initialized. Please invoke Init() before calling AddPossibleTarget.");
        }
        
        PossibleTarget.Add(damageable);

        List<IDamageable> list = PossibleTarget.ToList();
        list.Sort(new DamageablePriorityComparer());

        CurrentTarget = list[0].Entity;
    }

    private void RemovePossibleTarget(IDamageable damageable)
    {
        if (!_initialized)
        {
            throw new Exception("Sentry is not initialized. Please invoke Init() before calling RemovePossibleTarget.");
        }
        
        PossibleTarget.Remove(damageable);

        if (PossibleTarget.Count > 0)
        {
            List<IDamageable> list = PossibleTarget.ToList();
            list.Sort(new DamageablePriorityComparer());

            CurrentTarget = list[0].Entity;
        }
        else
        {
            CurrentTarget = null;
        }
    }

    private void OnDurationPassed()
    {
        AbilitySentryGun.SentryGunOnFieldCurrentNumber--;
        AbilitySentryGun.SentryGunsOnField.Remove(this);
    }

    #region IProjectileLauncher Properties and Methods
    
    public Func<bool> LaunchTriggerCondition { get; set; }

    public Action BeforeCheckLaunchCondition { get; set; }

    public Func<bool> PrimaryLaunchCondition { get; set; }

    public Func<bool> SecondaryLaunchCondition { get; set; }

    public Action BeforeLaunch { get; set; }

    public Action OnLaunch { get; set; }

    public Action AfterLaunch { get; set; }

    public Action<GameObject> OnHit { get; set; }

    public Projectile Projectile => AbilitySentryGun.Projectile;
    public ProjectilePattern ProjectilePattern => AbilitySentryGun.ProjectilePattern;

    public void Launch()
    {
        if (!_initialized)
        {
            throw new Exception("Sentry is not initialized. Please invoke Init() before calling RemovePossibleTarget.");
        }
        
        StartCoroutine(Fire());
    }

    private IEnumerator Fire()
    {
        if (CurrentTarget == null)
        {
            yield break;
        }
        
        Vector3 currentTargetPos = CurrentTarget.transform.position;
        
        for (int round = 0; round < ProjectilePattern.RoundsOfProjectile.Count; round++)
        {
            yield return new WaitForSeconds(ProjectilePattern.RoundsOfProjectile[round]
                .IntervalBetweenPreviousRound);
            
            Projectile[] projectiles = AbilitySentryGun.GetProjectile(ProjectilePattern.RoundsOfProjectile[round].Projectiles.Count, this);

            for (int projectileIndex = 0;
                 projectileIndex < ProjectilePattern.RoundsOfProjectile[round].Projectiles.Count;
                 projectileIndex++)
            {
                Projectile currentProjectile = projectiles[projectileIndex];
                Transform currentProjectileTransform = currentProjectile.transform;
                currentProjectileTransform.SetParent(transform);
                currentProjectileTransform.position = gameObject.transform.position;
                currentProjectile.OnHit += OnHitSomething;
                currentProjectile.Rb.velocity = (currentTargetPos - currentProjectile.transform.position).normalized *
                                                AbilitySentryGun.ProjectileTravelSpeed;
            }
        }
    }
    
    #endregion
    
    private void OnHitSomething(GameObject other, Projectile projectile)
    {
        Debug.Log("Hit something");

        // If hit an obstacle
        if (other.layer == LayerMask.NameToLayer("Obstacle"))
        {
            _projectilePool.Release(projectile);
            return;
        }

        // If hit an enemy
        if (other.CompareTag("Enemy"))
        {
            if (other.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(AbilitySentryGun.Damage);
            }
            else
            {
                Debug.Log("Enemy is invincible.");
            }
            
            _projectilePool.Release(projectile);
        }
    }
}
