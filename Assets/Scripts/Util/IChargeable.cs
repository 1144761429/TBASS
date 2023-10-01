using System;

public interface IChargeable
{
    Func<bool> ChargeCondition { get; set; }
    Action ChargeStartCallback { get; set; }
    Action OnChargingCallback { get; set; }
    Action FullyChargedCallback { get; set; }
    Action ChargeCancelCallback { get; set; }
    Action OnPauseCallback { get; set; }
    Action OnResumeCallback { get; set; }


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
    bool IsPaused { get; }

    float CurrentChargePercentage => ChargeProgress / ChargeThreshold;

    /// <summary>
    /// Reset the progress of the charger.
    /// </summary>
    void Reset();

    void SetIsPaused(bool isPaused);
}