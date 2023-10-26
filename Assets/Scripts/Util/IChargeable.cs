using System;

public interface IChargeable
{
    Func<bool> ChargeCondition { get; set; }
    Action OnChargeStart { get; set; }
    Action OnCharging { get; set; }
    Action OnFullyCharged { get; set; }
    Action OnChargeCancel { get; set; }
    Action OnFreeze { get; set; }
    Action OnUnfreeze { get; set; }


    /// <summary>
    /// How much progress has been charged.
    /// </summary>
    float ChargeProgress { get; set; }

    /// <summary>
    /// Unit of <c>ChargeSpeed</c> is "unit(s) per second".
    /// </summary>
    float ChargeSpeed { get; }

    /// <summary>
    /// Unit of <c>ChargeThreshold</c> is "unit(s)".
    /// </summary>
    float ChargeThreshold { get; set; }

    bool CanBePaused { get; set; }
    bool IsFrozen { get; }

    /// <summary>
    /// Reset the progress of the charger.
    /// </summary>
    void Reset();

    void SetIsFrozen(bool isFrozen);
}