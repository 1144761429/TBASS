using UnityEngine;

namespace DefaultNamespace
{
    public class Timer
    {
        private MonoBehaviour _mono;
        
        public float Duration;
        public float RemainingTime;
        public bool UseRealTime;

        public Timer(float duration, bool useRealTime)
        {
            Duration = duration;
            RemainingTime = Duration;
            UseRealTime = useRealTime;
        }

        public void Start()
        {
            
        }
    }
}