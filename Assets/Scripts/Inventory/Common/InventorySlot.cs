using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UISystem;

public class InventorySlot : MonoBehaviour
{
    public WrappedItem Item { get; private set; }

    public ItemData ItemData
    {
        get => Item.Data;
    }

    public int Amount
    {
        get => Item.Amount.Value;
    }

    [SerializeField] private Image _itemIcon;
    [SerializeField] private TMP_Text _amountText;

    [SerializeField] private InventoryInspectionSection InspectionSection;

    public bool IsOccupied
    {
        get => Item != null;
    }

    public void Store(WrappedItem wrappedItem)
    {
        if (wrappedItem == null)
        {
            throw new Exception("WrappedItem is null. Storing a null wrappedItem is not allowed.");
        }

        Item = wrappedItem;

        UpdateSlotVisual();
    }

    public void Clear()
    {
        Item = null;

        UpdateSlotVisual();

        Debug.Log($"{gameObject.name} is set to empty");
    }

    public void UpdateSlotVisual()
    {
        if (Item == null)
        {
            print("No item contained in this slot, no more update");
            _itemIcon.sprite = null;
            _amountText.text = "";
        }
        else
        {
            Sprite sprite = Resources.Load<Sprite>(ItemData.SpritePath);
            if (sprite == null)
            {
                throw new Exception(
                    $"Update slot visual failed due to sprite path for item Name: {ItemData.Name} ID: {ItemData.ID} point to nothing.");
            }

            _itemIcon.sprite = Resources.Load<Sprite>(ItemData.SpritePath);
            _amountText.text = Amount.ToString();
        }
    }

    public void OnClickSlot()
    {
        if (Item == null)
        {
            print($"Slot is empty. No info will be displayed.");
            return;
        }

        print($"Slot containing {ItemData?.Name} clicked.");
        InspectionSection.UpdateVisual(Item);
    }
}