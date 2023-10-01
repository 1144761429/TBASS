using BuffSystem.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class PanelPlayerBuffSlot : MonoBehaviour
    {
        /// <summary>
        /// The icon of a buff.
        /// </summary>
        [SerializeField] private Image icon;

        /// <summary>
        /// The TMP_Text component for displaying information, names plus remaining time, of a buff.
        /// </summary>
        [SerializeField] private TMP_Text text;

        /// <summary>
        /// The buff being stored in the buff slot.
        /// </summary>
        private Buff _buff;

        /// <summary>
        /// Update the buff that is held by a buff slot, including updating the icon and the time to its duration.
        /// </summary>
        /// <param name="buff"></param>
        public void UpdateVisual(Buff buff)
        {
            _buff = buff;
            icon.sprite = _buff.Icon;
            text.text = $"{_buff.Name} + {_buff.Duration}";
        }

        /// <summary>
        /// Update the displaying remaining time of a buff.
        /// </summary>
        public void UpdateRemainingTime()
        {
            //DurationTimer could be null
            float remainingTimeWith2Digits = (int)(_buff.DurationTimer.GetTimeRemaining() * 10) / 10.0f;
            text.text = $"{_buff.Name} {remainingTimeWith2Digits}";
        }
    }
}