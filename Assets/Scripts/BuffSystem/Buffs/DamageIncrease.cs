using BuffSystem.Common;
using BuffSystem.Interface;
using UnityEngine;

namespace BuffSystem.Buffs
{
    public class DamageIncrease : Buff, IDisplayable, IFinalDmgModificationBuff
    {
        public int Priority => 3;
        public float FinalDmgModFixedValue => 10;
        public float FinalDmgModPercentageValue => 0;


        public DamageIncrease(IBuffable target, float duration, BuffHandler buffHandler) : base(
            EBuffName.DamageIncrease,
            target,
            duration, false, false,
            false, 1, 0, 1,
            buffHandler)
        {
            RemoveCallback += () => { Debug.Log("FinalDmgMod buff ended"); };
        }
    }
}