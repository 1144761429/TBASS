using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class PanelPlayerHealthBar : PanelBase
    {
        [SerializeField] private Image healthFill;
        [SerializeField] private Image shieldFill;

        private PlayerDataSO _data;

        private void Awake()
        {
            _data = PlayerStats.Instance.RuntimeData;
        }

        private void OnEnable()
        {
            PlayerStats.Instance.HpChangeCallback += UpdateHealthVisual;
            PlayerStats.Instance.ShieldChangeCallback += UpdateShieldVisual;
        }

        private void UpdateHealthVisual()
        {
            healthFill.fillAmount = _data.CurrentHp / _data.MaxHp;
        }

        private void UpdateShieldVisual()
        {
            shieldFill.fillAmount = _data.CurrentSheild / _data.MaxShield;
        }
    }
}