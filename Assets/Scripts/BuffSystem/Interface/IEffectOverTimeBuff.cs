using System;
using UnityTimer;

namespace BuffSystem.Interface
{
    public interface IEffectOverTimeBuff
    {
        Action OnTick { get; }

        float TickCount { get; }
    }
}