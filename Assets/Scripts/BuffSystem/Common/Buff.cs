using System;
using BuffSystem.Interface;
using StackableElement;
using UnityEngine;
using UnityEngine.UI;
using UnityTimer;

namespace BuffSystem.Common
{
    /// <summary>
    /// The base class for all the buffs.
    /// A buff in the game must inherits this class, and may have many other interfaces as well. E.g., IFinalDmgBuff, IEffectOverTimeBuff, etc.
    /// </summary>
    public abstract class Buff : IStackable
    {
        public Action TriggerCallback;
        public Action BuffEffectingCallback;
        public Action RemoveCallback;

        public EBuffName Name { get; protected set; }

        public IBuffable Target { get; protected set; }

        public float Duration { get; protected set; }
        public Timer DurationTimer { get; protected set; }
        public int Stack { get; protected set; }
        public int MinStack { get; protected set; }
        public int MaxStack { get; protected set; }
        public bool IsStackable { get; protected set; }
        public bool IsFrozen { get; protected set; }
        protected bool IsRealtime;
        protected bool IsLoop;

        public Sprite Icon
        {
            get
            {
                if (_icon == null)
                {
                    _icon = DatabaseUtil.Instance.GetBuffIcon(Name);
                }

                return _icon;
            }
        }

        private Sprite _icon;


        private BuffHandler _buffHandler;

        protected Buff(EBuffName name,
            IBuffable target,
            float duration, bool isRealTime, bool isLoop,
            bool isStackable, int initStack, int minStack, int maxStack,
            BuffHandler buffHandler)
        {
            if (initStack < minStack || initStack > maxStack)
            {
                throw new ArgumentException("Trying to set the initial stack to a number that is out of range." +
                                            $"\nInitStack: {initStack}, MinStack: {minStack}, MaxStack: {maxStack}.");
            }

            Name = name;
            Target = target;
            Duration = duration;
            IsStackable = isStackable;
            IsRealtime = isRealTime;
            IsLoop = isLoop;

            if (isStackable)
            {
                MinStack = minStack;
                MaxStack = maxStack;
            }
            else
            {
                if (MinStack != 0 && MaxStack != 1)
                {
                    Debug.LogError(
                        $"Buff {name} is not stackable but the min and max stack are set to {minStack} and {maxStack}.");
                }

                MinStack = 0;
                MaxStack = 1;
            }

            Stack = initStack;
            IsFrozen = false;
            _buffHandler = buffHandler;
        }

        public virtual void Init()
        {
        }

        public virtual void Trigger()
        {
            if (Duration <= int.MaxValue)
            {
                DurationTimer = Timer.Register(Duration, () => _buffHandler.RemoveBuff(this), isLooped: IsLoop,
                    useRealTime: IsRealtime);
            }
        }
    }
}