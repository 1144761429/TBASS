using System;
using BuffSystem.Common;
using BuffSystem.Interface;
using UnityEngine;
using UnityEngine.UI;
using UnityTimer;

namespace BuffSystem.Buffs
{
    public class Bleed : Buff, IDisplayable, IEffectOverTimeBuff, IDamagingBuff
    {
        public Action OnTick { get; private set; }

        public int Priority => 2;

        public float TickCount { get; private set; }
        public Timer TickTimer { get; private set; }

        public float Damage { get; private set; }
        public IDamageable DamageableTarget { get; private set; }

        public Bleed(IBuffable target, float damage, float tickTime, float tickCount,
            BuffHandler buffHandler) : base(
            EBuffName.Bleed, target,
            tickTime, false, true,
            false, 1, 0, 1,
            buffHandler)
        {
            DamageableTarget = (IDamageable)Target;
            Damage = damage;
            TickCount = tickCount;

            OnTick += DoBleedDamage;
        }

        public override void Trigger()
        {
            TickTimer = Timer.Register(Duration, OnTick, isLooped: true);
        }


        private void DoBleedDamage()
        {
            if (TickCount <= 0)
            {
                TickTimer.Cancel();
                return;
            }

            TickCount--;

            DamageableTarget.TakeDamage(Damage);
            Debug.Log("Bleeding");
        }

        private void OnDurationElapsed()
        {
            TickTimer.Cancel();
            Debug.Log("Duration elapsed");
        }
    }
}