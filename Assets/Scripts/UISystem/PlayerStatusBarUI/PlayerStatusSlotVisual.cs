using BuffSystem.Common;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem
{
    public class PlayerStatusSlotVisual : UIVisual
    {
        /// <summary>
        /// The icon of a buff.
        /// </summary>
        [SerializeField] private Image icon;

        /// <summary>
        /// The TMP_Text component for displaying information, names plus remaining time, of a buff.
        /// </summary>
        [SerializeField] private TMP_Text remainingTimeText;
        
        /// <summary>
        /// Update the buff that is held by a buff slot, including updating the icon and the time to its duration.
        /// </summary>
        /// <param name="status"></param>
        public void UpdateVisual(Buff status)
        {
            icon.sprite = status.Icon;
            remainingTimeText.text = $"{status.Name} + {status.Duration}";
        }

        /// <summary>
        /// Update the displaying remaining time of a buff.
        /// </summary>
        public void UpdateRemainingTime(Buff status)
        {
            //DurationTimer could be null
            float remainingTimeWith2Digits = (int)(status.DurationTimer.GetTimeRemaining() * 10) / 10.0f;
            remainingTimeText.text = $"{status.Name} {remainingTimeWith2Digits}";
        }
    }
}