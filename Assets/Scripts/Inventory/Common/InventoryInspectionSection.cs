using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryInspectionSection : MonoBehaviour
{
    [SerializeField] private Image _itemIconBackground;
    [SerializeField] private Image _itemIconImage;

    [SerializeField] private TMP_Text _itemName;
    [SerializeField] private TMP_Text _itemAmount;
    [SerializeField] private TMP_Text _itemDescription;

    public void UpdateVisual(WrappedItem wrappedItem)
    {
        //TODO:
        //Set _itemIconBackground

        _itemIconImage.sprite = Resources.Load<Sprite>(wrappedItem.Data.SpritePath);

        _itemName.text = wrappedItem.Name;
        _itemAmount.text = wrappedItem.Amount.ToString();
        _itemDescription.text = $"This is a {wrappedItem.Name}.";
    }
}
