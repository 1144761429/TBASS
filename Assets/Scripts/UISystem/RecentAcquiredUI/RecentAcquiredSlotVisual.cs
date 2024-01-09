using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditorInternal;

namespace UISystem
{
    public class RecentAcquiredSlotVisual : UIVisual
    {
        [SerializeField] private Image _itemIcon;
        [SerializeField] private TMP_Text _text;

        public void UpdateVisual(object sender, CollecetItemEventArgs args)
        {
            WrappedItem item = args.Item;

            _itemIcon.sprite = Resources.Load<Sprite>(item.StaticData.SpritePath);
            _text.text = item.Amount.ToString();
        }
    }
}