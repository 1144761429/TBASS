using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class PlayerHealthBarVisual : UIVisual
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private Image shieldFill;

        private void OnEnable()
        {
            PlayerStats.Instance.OnHealthChange += UpdateHealthVisual;
            PlayerStats.Instance.OnArmorChange += UpdateArmorVisual;
        }

        public void UpdateHealthVisual(object sender, HealthChangeEventArgs args)
        {
            healthFill.fillAmount = args.HealthAfterChange / args.MaxHealth;
        }

        public void UpdateArmorVisual(object sender, ArmorChangeEventArgs args)
        {
            shieldFill.fillAmount = args.ArmorAfterChange / args.MaxArmor;
        }
    }
}