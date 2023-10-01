using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UISystem;

public class PlayerInventory
{
    public static PlayerInventory Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new PlayerInventory();
            }

            return _instance;
        }
    }

    private static PlayerInventory _instance;

    public InventoryEquipment Equipments { get; private set; }
    public InventoryMaterial Materials { get; private set; }
    public InventoryConsumable Consumables { get; private set; }

    public PanelPlayerInventory UI { get; private set; }

    public PlayerInventory()
    {
        Init();
    }

    private void Init()
    {
        UI = Resources.Load<GameObject>("Prefabs/UI/ScreenSpace/PanelPlayerInventory")
            .GetComponent<PanelPlayerInventory>();
        //Consumables = GetComponentInChildren<InventoryConsumable>();
        Consumables = GameObject.FindWithTag("PlayerInventory").GetComponentInChildren<InventoryConsumable>();
    }

    private void Awake()
    {
        Init();
    }

    public void SetupSlots(PanelPlayerInventory panelPlayerInventory)
    {
        Consumables.SetupSlots(panelPlayerInventory.ConsumablesTabSlots);

        //TODO: implement materials and equipments
    }

    public bool AddItem(WrappedItem wrappedItem)
    {
        if (wrappedItem.Data == null)
        {
            Debug.LogWarning("Adding to inventory failed due to the item data is null");
            return false;
        }

        switch (wrappedItem.Type)
        {
            case EItemType.Equipment:
                throw new NotImplementedException();
            case EItemType.Material:
                throw new NotImplementedException();
            case EItemType.Consumable:
                return Consumables.AddItem(new WrappedItem<ItemDataConsumable>(
                    DatabaseUtil.Instance.GetItemDataConsumableSupply(wrappedItem.ID), wrappedItem.Amount));
            default:
                throw new Exception(
                    $"No matching enum for wrappedItem Name: {wrappedItem.Name}, ID: {wrappedItem.ID}, Type: {wrappedItem.Type}.");
        }
    }

    #region Debug Helper

    private void DebugIsOccupiedInfo()
    {
        foreach (var slot in UI.ConsumablesTabSlots)
        {
            if (slot.IsOccupied)
            {
                Debug.Log(slot.gameObject.name);
            }
        }
    }

    #endregion
}