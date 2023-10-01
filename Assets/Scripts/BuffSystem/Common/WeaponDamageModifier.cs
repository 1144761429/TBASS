using System.Collections.Generic;
using BuffSystem.Interface;

namespace BuffSystem.Common
{
    public class WeaponDamageModifier
    {
        private readonly List<IWeaponDmgModificationBuff> _buffs;

        public WeaponDamageModifier()
        {
            _buffs = new List<IWeaponDmgModificationBuff>();
        }

        public float GetWeaponDmgModValue()
        {
            float result = 0;
            foreach (var buff in _buffs)
            {
                result += buff.WeaponDmgModPercentageValue * ((Buff)buff).Stack;
            }

            return result;
        }

        public void Add(IWeaponDmgModificationBuff modificationBuff)
        {
            _buffs.Add(modificationBuff);
        }

        public void Remove(IWeaponDmgModificationBuff modificationBuff)
        {
            _buffs.Remove(modificationBuff);
        }

        public bool Contains(IWeaponDmgModificationBuff modificationBuff)
        {
            return _buffs.Contains(modificationBuff);
        }
    }
}