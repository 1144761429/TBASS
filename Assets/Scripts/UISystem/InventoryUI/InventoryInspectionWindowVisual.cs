using TMPro;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UI;

namespace UISystem.InventoryUI
{
    
    public class InventoryInspectionWindowVisual : UIVisual
    {
        [field: SerializeField] public TMP_Text ItemName { get; private set; }
        [field: SerializeField] public TMP_Text ItemAmount{ get; private set; }
        [field: SerializeField] public TMP_Text ItemDescription{ get; private set; }
        [field: SerializeField] public Image ItemIcon{ get; private set; }
        
        [field: SerializeField] public Button LeftButton { get; private set; }
        [field: SerializeField] public Button RightButton{ get; private set; }
        public TMP_Text LeftButtonText { get; private set; }
        public TMP_Text RightButtonText { get; private set; }

        private void Awake()
        {
            LeftButtonText = LeftButton.GetComponentInChildren<TMP_Text>();
            RightButtonText = RightButton.GetComponentInChildren<TMP_Text>();

            HideButtons();
        }

        public void UpdateVisual(WrappedItem item)
        {
            if (item == null)
            {
                Reset();
                return;
            }
            
            Sprite icon = Resources.Load<Sprite>(item.StaticData.SpritePath);
            if (icon == null)
            {
                throw new InvalidPathException("The path of the sprite is invalid.");
            }

            ItemIcon.sprite = icon;

            ItemName.text = item.Name;
            ItemAmount.text = item.Amount.ToString();
            ItemDescription.text = $"This is a {item.Name}."; // TODO: change it to show description.
            
            // TODO: implement the left and right buttons.
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
            LeftButtonText.text = "Remove";
        }

        public void Reset()
        {
            ItemName.text = "";
            ItemAmount.text = "";
            ItemDescription.text = "";
            ItemIcon.sprite = null;
        }

        public void HideButtons()
        {
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(false);
        }
    }
}