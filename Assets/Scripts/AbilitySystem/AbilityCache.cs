using UnityEngine;
using UnityEngine.UI;

namespace AbilitySystem
{
    public class AbilityCache
    {
        public static AbilityCache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AbilityCache();
                }

                return _instance;
            }
        }
        
        private static AbilityCache _instance;

        public AbilitySentryGun SentryGun;

        private AbilityCache()
        {
            Init();
        }
        
        private void Init()
        {
            SentryGun = Resources.Load<AbilitySentryGun>("ScriptableObjects/Abilities/SentryGun");
        }
    }
}