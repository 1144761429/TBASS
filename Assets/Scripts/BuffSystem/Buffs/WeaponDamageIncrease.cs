using BuffSystem.Common;
using BuffSystem.Interface;
using UnityEngine;

namespace BuffSystem.Buffs
{
    public class WeaponDamageIncrease : Buff, IDisplayable, IWeaponDmgModificationBuff
    {
        public int Priority => 3;
        public float WeaponDmgModFixedValue => 20;
        public float WeaponDmgModPercentageValue => 0;

        public WeaponDamageIncrease(IBuffable target, float duration, BuffHandler buffHandler) : base(
            EBuffName.WeaponDamageIncrease,
            target,
            duration, false, false,
            false, 1, 0, 1,
            buffHandler)
        {
            RemoveCallback += () => { Debug.Log("WeaponDmgMod buff ended"); };
        }
    }
}