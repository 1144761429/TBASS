using System;
using System.Collections.Generic;
using BuffSystem.Interface;
using JetBrains.Annotations;
using ArgumentNullException = System.ArgumentNullException;

namespace BuffSystem.Common
{
    /// <summary>
    /// A handler for buffs,
    /// including features like adding and removing <c>Buff</c>,
    /// calculating the overall value of a same type of <c>Buff</c>, etc.
    /// </summary>
    public class BuffHandler
    {
        /// <summary>
        /// Subscribe to this event to perform actions when a buff is added.
        /// </summary>
        public event Action<Buff> AddBuffCallback;

        /// <summary>
        /// Subscribe to this event to perform actions when a buff is removed.
        /// </summary>
        public event Action<Buff> RemoveBuffCallback;

        public FinalDamageModifier FinalDamageModifier { get; private set; }
        public WeaponDamageModifier WeaponDamageModifier { get; private set; }


        /// <summary>
        /// A list that stores all the <c>Buff</c> in a <c>BuffHandler</c>.
        /// </summary>
        private readonly List<Buff> _buffs;

        public BuffHandler()
        {
            FinalDamageModifier = new FinalDamageModifier();
            WeaponDamageModifier = new WeaponDamageModifier();
            _buffs = new List<Buff>();
        }

        /// <summary>
        /// Add a buff to the BuffHandler
        /// </summary>
        /// <param name="buff">The buff being added</param>
        /// <param name="triggerAfterAdd">
        /// If true, the buff will immediately take effect after addition.
        /// If false, the buff will not immediately take effect after addition.
        /// </param>
        public void AddBuff([NotNull] Buff buff, bool triggerAfterAdd)
        {
            _buffs.Add(buff);
            ClassifyBuff(buff);
            AddBuffCallback?.Invoke(buff);

            if (triggerAfterAdd)
            {
                buff.Trigger();
            }
        }

        public void RemoveBuff([NotNull] Buff buff)
        {
            _buffs.Remove(buff);
            buff.RemoveCallback?.Invoke();
            RemoveBuffCallback?.Invoke(buff);

            if (buff is IFinalDmgModificationBuff)
            {
                FinalDamageModifier.Remove((IFinalDmgModificationBuff)buff);
            }
        }

        /// <summary>
        /// Add a reference of the buff to some lists according to the interface it inherits.
        /// This helps to arrange different buff that shares the same functionalities.
        /// </summary>
        /// <example>
        /// TODO: give an example
        /// </example>
        /// <param name="buff">The buff to be classified</param>
        private void ClassifyBuff(Buff buff)
        {
            if (buff is IFinalDmgModificationBuff && !FinalDamageModifier.Contains((IFinalDmgModificationBuff)buff))
            {
                FinalDamageModifier.Add((IFinalDmgModificationBuff)buff);
            }

            //TODO: add more classification
        }
    }
}