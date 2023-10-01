using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UISystem
{
    public class PanelRecentAcquiredSlot : MonoBehaviour
    {
        [SerializeField] private Image _itemIcon;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponentInChildren<TMP_Text>();
        }

        public void UpdateVisual(WrappedItem wrappedItem)
        {
            _itemIcon.sprite = Resources.Load<Sprite>(wrappedItem.Data.SpritePath);
            _text.text = wrappedItem.Amount.Value.ToString();
        }
    }
}