using System;
using BuffSystem.Buffs;
using BuffSystem.Common;
using Characters.Player.Data;
using UISystem;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerStats : MonoBehaviour, IBuffable
{
    public static PlayerStats Instance { get; private set; }

    /// <summary>
    /// Subscribe to this event to perform actions when player's HP changes.
    /// </summary>
    public event EventHandler<HealthChangeEventArgs> OnHealthChange;

    /// <summary>
    /// Subscribe to this event to perform actions when player's shield changes.
    /// </summary>
    public event EventHandler<ArmorChangeEventArgs> OnArmorChange;

    /// <summary>
    /// The original data of player in form of <c>ScriptableObject</c>.
    /// When game starts, we create a copy of the sourceData.
    /// This helps to avoid directly manipulating the source data.
    /// So, we don't need to reset the source data to what it was like before game starts every time.
    /// </summary>
    [SerializeField] private PlayerDataSO staticData;

    /// <summary>
    /// The copy, which we manipulate in game, of sourceData.
    /// </summary>
    public RuntimePlayerData RuntimeData { get; private set; }

    public bool CanTakeBuff => true;
    public bool IsBleedResist => false;
    public BuffHandler BuffHandler { get; private set; }


    private void Awake()
    {
        InitSingleton();

        // Copy the sourceData and initialize the RuntimeData
        RuntimeData = new RuntimePlayerData(staticData);

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


    public void AddHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Health amount to add must be non-negative.", nameof(amount));
        }

        float healthBeforeChange = RuntimeData.CurrentHealth;
        
        overflowAmount = Mathf.Max(0, RuntimeData.CurrentHealth + amount - staticData.MaxHP);
        float actualAmountToAdd = amount - overflowAmount;
        RuntimeData.AddHealth(actualAmountToAdd);

        HealthChangeEventArgs args =
            new HealthChangeEventArgs(healthBeforeChange, RuntimeData.CurrentHealth, staticData.MaxHP);

        if (triggerHpChangeEvent) OnHealthChange?.Invoke(this, args);
    }

    public void ReduceHp(float amount, out float overflowAmount, bool triggerHpChangeEvent = true)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Health amount to reduce must be non-negative.", nameof(amount));
        }

        float healthBeforeChange = RuntimeData.CurrentHealth;
        
        overflowAmount = Mathf.Min(0, RuntimeData.CurrentHealth - staticData.MinHP - amount);
        float actualAmountToReduce = amount - overflowAmount;
        RuntimeData.ReduceHealth(actualAmountToReduce);

        HealthChangeEventArgs args =
            new HealthChangeEventArgs(healthBeforeChange, RuntimeData.CurrentHealth, staticData.MaxHP);
        
        if (triggerHpChangeEvent) OnHealthChange?.Invoke(this, args);
    }

    public void AddArmor(float amount, out float overflowAmount, bool triggerArmorChangeEvent = true)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Armor amount to add must be non-negative.", nameof(amount));
        }

        float armorBeforeChange = RuntimeData.CurrentArmor;
        
        overflowAmount = Mathf.Max(0, RuntimeData.CurrentArmor + amount - staticData.MaxArmor);
        float actualAmountToAdd = amount - overflowAmount;
        RuntimeData.AddArmor(actualAmountToAdd);

        ArmorChangeEventArgs args =
            new ArmorChangeEventArgs(armorBeforeChange, RuntimeData.CurrentArmor, staticData.MaxArmor);
        
        if (triggerArmorChangeEvent) OnArmorChange?.Invoke(this, args);
    }

    public void ReduceArmor(float amount, out float overflowAmount, bool triggerShieldChangeEvent = true)
    {
        if (amount < 0)
        {
            throw new ArgumentException("Armor amount to reduce must be non-negative.", nameof(amount));
        }

        float armorBeforeChange = RuntimeData.CurrentArmor;
        
        overflowAmount = Mathf.Min(0, RuntimeData.CurrentArmor - staticData.MinArmor - amount);
        float actualAmountToReduce = amount - overflowAmount;
        RuntimeData.ReduceArmor(actualAmountToReduce);

        ArmorChangeEventArgs args =
            new ArmorChangeEventArgs(armorBeforeChange, RuntimeData.CurrentArmor, staticData.MaxArmor);

        if (triggerShieldChangeEvent) OnArmorChange?.Invoke(this, args);
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