using System;

[Serializable]
public class ItemDataConsumableSupply : ItemDataConsumable
{

    public float HPRecovery;
    public float HPRecPerSec;
    public float HPRecDuration;

    public float ShieldRec;
    public float ShieldRecPerSec;
    public float ShieldRecDuration;

    public float AttackPctInc;
    public float MovementSpeedPctInc;


    public override string ToString()
    {
        return base.ToString()
        + $"HPRecovery: {HPRecovery}\n"
        + $"HPRecPerSec: {HPRecPerSec}\n"
        + $"HPRecDuration: {HPRecDuration}\n"
        + $"ShieldRec: {ShieldRec}\n"
        + $"ShieldRecPerSec: {ShieldRecPerSec}\n"
        + $"ShieldRecDuration: {ShieldRecDuration}\n"
        + $"AttackPctInc: {AttackPctInc}\n"
        + $"MovementSpeedPctInc: {MovementSpeedPctInc}\n";
    }
}
