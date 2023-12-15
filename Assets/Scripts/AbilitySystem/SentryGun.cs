using System.Collections.Generic;
using AbilitySystem;
using BehaviorDesigner.Runtime;
using CombatSystem;
using UnityEngine;
using UnityTimer;

public class SentryGun : MonoBehaviour
{
    public IAbilityCaster Caster;
    public AbilitySentryGun AbilitySentryGun;
    
    public List<GameObject> PossibleTargetGameObjects { get; private set; }
    public HashSet<IDamageable> PossibleTarget { get; private set; }
    public GameObject CurrentTarget { get; private set; }

    public float Duration => _timer.duration;
    public float RemainingDuration => _timer.GetTimeRemaining();
    
    [field: SerializeField] public float PreparationTime { get; private set; }
    
    private Timer _timer;
    private Collider2D _collider;
    private BehaviorTree _behaviorTree;
    
    #region MonoBehaviour Methods
    
    private void Awake()
    {
        PossibleTargetGameObjects = new List<GameObject>();
        PossibleTarget = new HashSet<IDamageable>();
        
        _collider = GetComponent<Collider2D>();
        _behaviorTree = GetComponent<BehaviorTree>();
    }
    
    private void Update()
    {
        // Debug.Log(CurrentTarget.name);
        
        // foreach (var target in PossibleTargetGameObjects)
        // {
        //     print(target.gameObject.name);
        // }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }
        
        PossibleTargetGameObjects.Add(damageable.Entity);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent(out IDamageable damageable))
        {
            return;
        }

        AddPossibleTarget(damageable);
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

    public void Init(AbilitySentryGun abilitySentryGun, IAbilityCaster caster)
    {
        AbilitySentryGun = abilitySentryGun;
        Caster = caster;
    }
    
    /// <summary>
    /// Register a timer that handles the duration of the sentry gun.
    /// </summary>
    /// <param name="time"></param>
    public void StartCountDown(float time)
    {
        // When the time completely elapse, invoke relevant event method.
        _timer = Timer.Register(time, OnDurationPassed);
    }

    /// <summary>
    /// Extend the duration of the sentry gun based on the remaining duration.
    /// </summary>
    /// <param name="extendedTime">Time to extend.</param>
    public void ExtendDuration(float extendedTime)
    {
        _timer.Cancel();
        _timer = Timer.Register(_timer.GetTimeRemaining() + extendedTime, null);
    }

    private void AddPossibleTarget(IDamageable damageable)
    {
        if (PossibleTarget.Add(damageable))
        {
            PossibleTargetGameObjects.Add(damageable.Entity);
            PossibleTargetGameObjects.Sort(new DamageablePriorityComparer());

            CurrentTarget = PossibleTargetGameObjects[0];
        }
    }

    private void RemovePossibleTarget(IDamageable damageable)
    {
        if (!PossibleTarget.Remove(damageable))
        {
            PossibleTargetGameObjects.Remove(damageable.Entity);
            PossibleTargetGameObjects.Sort(new DamageablePriorityComparer());
            CurrentTarget = PossibleTargetGameObjects[0];
        }
    }

    private void OnDurationPassed()
    {
        AbilitySentryGun.CurrentSentryGunOnField--;
        AbilitySentryGun.SentryGunsOnField.Remove(this);
    }
}
