using UnityEngine;
using System;

namespace WeaponSystem
{
    public class AnimationEventHandler : MonoBehaviour
    {
        public event Action OnFinished;

        private void AnimationFinishedTrigger() => OnFinished?.Invoke();
    }
}
