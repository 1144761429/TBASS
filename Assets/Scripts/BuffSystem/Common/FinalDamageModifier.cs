using System.Collections.Generic;
using BuffSystem.Interface;

namespace BuffSystem.Common
{
    public class FinalDamageModifier
    {
        private readonly List<IFinalDmgModificationBuff> _buffs;

        public FinalDamageModifier()
        {
            _buffs = new List<IFinalDmgModificationBuff>();
        }

        public float GetFinalDmgModValue()
        {
            float result = 0;
            foreach (var buff in _buffs)
            {
                result += buff.FinalDmgModPercentageValue * ((Buff)buff).Stack;
            }

            return result;
        }

        public void Add(IFinalDmgModificationBuff modificationBuff)
        {
            _buffs.Add(modificationBuff);
        }

        public void Remove(IFinalDmgModificationBuff modificationBuff)
        {
            _buffs.Remove(modificationBuff);
        }

        public bool Contains(IFinalDmgModificationBuff modificationBuff)
        {
            return _buffs.Contains(modificationBuff);
        }
    }
}