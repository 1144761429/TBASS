using System;
using BuffSystem.Buffs;
using BuffSystem.Common;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour, IBuffable
{
    public static PlayerStats Instance { get; private set; }

    /// <summary>
    /// Subscribe to this event to perform actions when player's HP changes.
    /// </summary>
    public event Action HpChangeCallback;

    /// <summary>
    /// Subscribe to this event to perform actions when player's shield changes.
    /// </summary>
    public event Action ShieldChangeCallback;

    /// <summary>
    /// The original data of player in form of <c>ScriptableObject</c>.
    /// When game starts, we create a copy of the sourceData.
    /// This helps to avoid directly manipulating the source data.
    /// So, we don't need to reset the source data to what it was like before game starts every time.
    /// </summary>
    [SerializeField] private PlayerDataSO sourceData;

    /// <summary>
    /// The copy, which we manipulate in game, of sourceData.
    /// </summary>
    public PlayerDataSO RuntimeData { get; private set; }

    public bool CanTakeBuff => true;
    public bool IsBleedResist => false;
    public BuffHandler BuffHandler { get; private set; }


    private void Awake()
    {
        InitSingleton();

        // Copy the sourceData and initialize the RuntimeData
        RuntimeData = Instantiate(sourceData);
        RuntimeData.Init();

        BuffHandler = new BuffHandler();
    }

    // ========================================Test Purpose========================================
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            DamageIncrease di = new DamageIncrease(this, 3, BuffHandler);
            BuffHandler.AddBuff(di, true);
        }
    }
    // ========================================Test Purpose========================================

    /// <summary>
    /// Add a specific amount of hp to <c>CurrentHp</c>.
    /// If the amount to add is negative, then the method will automatically call <code>ReduceHp(amount * -1)</code>
    /// </summary>
    /// <param name="amount">Amount of hp to add.</param>
    /// <param name="triggerHpChangeEvent">True if should call <c>HpChangeCallback</c> after adding hp.</param>
    public void AddHp(float amount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            ReduceHp(amount);
            return;
        }

        RuntimeData.CurrentHp = Mathf.Min(RuntimeData.CurrentHp + amount, RuntimeData.MaxHp);

        if (triggerHpChangeEvent) HpChangeCallback?.Invoke();
    }

    /// <summary>
    /// Add a specific amount hp to <c>CurrentHp</c> and check if it will overflow (though the actual <c>CurrentHp</c> will not pass <c>MaxHp</c>).
    /// If the amount to add is negative, then the method will automatically call <code>ReduceHp(amount * -1, out overflowAmount)</code>
    /// </summary>
    /// <param name="amount">Amount of hp to add.</param>
    /// <param name="overflowAmount">Amount of hp that will overflow</param>
    /// <param name="triggerHpChangeEvent">True if should call <c>HpChangeCallback</c> after adding hp.</param>
    /// <returns>True if adding the specific amount of hp will cause an overflow. False if not. </returns>
    public bool AddHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            return ReduceHp(amount * -1, out overflowAmount);
        }

        float finalHp = Mathf.Min(RuntimeData.CurrentHp + amount, RuntimeData.MaxHp);
        overflowAmount = Mathf.Max(0, finalHp - RuntimeData.MaxHp);

        RuntimeData.CurrentHp = finalHp;

        if (triggerHpChangeEvent)
        {
            HpChangeCallback?.Invoke();
        }

        return overflowAmount > 0;
    }

    /// <summary>
    /// Reduce a specific amount of hp from <c>CurrentHp</c>.
    /// If the amount to reduce is negative, then the method will automatically call <code>AddHp(amount * -1)</code>
    /// </summary>
    /// <param name="amount">Amount of hp to reduce</param>
    /// <param name="triggerHpChangeEvent">True if should call <c>HpChangeCallback</c> after adding hp.</param>
    public void ReduceHp(float amount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            AddHp(amount);
            return;
        }

        RuntimeData.CurrentHp = Mathf.Max(RuntimeData.CurrentHp - amount, RuntimeData.MinHp);

        if (triggerHpChangeEvent) HpChangeCallback?.Invoke();
    }

    /// <summary>
    /// Reduce a specific amount of hp from <c>CurrentHp</c> and check if it will overflow (though the actual <c>CurrentHp</c> will not pass <c>MinHp</c>).
    /// If the amount to reduce is negative, then the method will automatically call <code>AddHp(amount * -1, out overflowAmount)</code>
    /// </summary>
    /// <param name="amount">Amount of hp to reduce</param>
    /// <param name="overflowAmount">Amount of hp that will overflow</param>
    /// <param name="triggerHpChangeEvent">True if should call <c>HpChangeCallback</c> after adding hp.</param>
    public bool ReduceHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            return AddHp(amount, out overflowAmount);
        }

        float finalHp = Mathf.Max(RuntimeData.CurrentHp - amount, RuntimeData.MinHp);
        overflowAmount = Mathf.Max(0, RuntimeData.MinHp - finalHp);

        RuntimeData.CurrentHp = finalHp;

        if (triggerHpChangeEvent)
        {
            HpChangeCallback?.Invoke();
        }

        return overflowAmount > 0;
    }

    public void AddCurrentShield(float amount, bool triggerShieldChangeEvent = true)
    {
        RuntimeData.CurrentSheild = RuntimeData.CurrentSheild + amount > RuntimeData.MaxShield
            ? RuntimeData.MaxShield
            : RuntimeData.CurrentSheild + amount;
        if (triggerShieldChangeEvent) ShieldChangeCallback?.Invoke();
    }

    public void ReduceCurrentShield(float amount, bool triggerShieldChangeEvent = true)
    {
        RuntimeData.CurrentSheild = RuntimeData.CurrentSheild - amount < RuntimeData.MinShield
            ? RuntimeData.MinShield
            : RuntimeData.CurrentSheild - amount;
        if (triggerShieldChangeEvent) ShieldChangeCallback?.Invoke();
    }

    private void InitSingleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
}