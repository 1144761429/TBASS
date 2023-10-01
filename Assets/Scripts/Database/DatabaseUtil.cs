using System;
using System.Collections.Generic;
using System.IO;
using BuffSystem.Common;
using Newtonsoft.Json;
using UnityEngine;


public class DatabaseUtil
{
    public static DatabaseUtil Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new DatabaseUtil();
            }

            return _instance;
        }
    }

    private static DatabaseUtil _instance;

    private Dictionary<int, ItemDataConsumableSupply> _itemConsumableSupplyDB;
    private Dictionary<int, ItemDataEquipmentWeapon> _itemEquipmentWeaponDB;

    private Dictionary<EBuffName, string> _buffIconPathDB;

    private DatabaseUtil()
    {
        InitDBDict();
    }

    //TODO: Find a generic method for fetching item data
    public ItemDataConsumableSupply GetItemDataConsumableSupply(int id)
    {
        if (_itemConsumableSupplyDB.TryGetValue(id, out ItemDataConsumableSupply itemData))
        {
            return itemData;
        }

        throw new Exception($"Retrieving data failed due to item id does not exist. ID: {id}");
    }

    public ItemDataEquipmentWeapon GetItemDataEquipmentWeapon(int id)
    {
        if (_itemEquipmentWeaponDB.TryGetValue(id, out ItemDataEquipmentWeapon itemData))
        {
            return itemData;
        }

        throw new Exception($"Retrieving data failed due to item id does not exist. ID: {id}");
    }

    public Sprite GetBuffIcon(EBuffName buffName)
    {
        if (_buffIconPathDB.TryGetValue(buffName, out string spritePath))
        {
            if (string.IsNullOrEmpty(spritePath))
            {
                throw new Exception(
                    "Sprite path is null or empty. Please check if the path is input in the json file." +
                    $"\nBuff Name: {buffName}.");
            }

            Sprite icon = Resources.Load<Sprite>(spritePath);

            if (icon == null)
            {
                throw new Exception(
                    "Sprite path is incorrect. Please check if the path is correct in the json file." +
                    $"\nBuff Name: {buffName}.");
            }

            return icon;
        }

        throw new Exception($"Retrieving buff icon failed due to buff name does not exist. Buff Name: {buffName}");
    }

    private void InitDBDict()
    {
        string jsonItemConsumableSupply =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Item\itemDataConfig_consumable_supply.json");
        List<ItemDataConsumableSupply> deserializedJsonItemConsumableSupply =
            JsonConvert.DeserializeObject<List<ItemDataConsumableSupply>>(jsonItemConsumableSupply);
        _itemConsumableSupplyDB = new Dictionary<int, ItemDataConsumableSupply>();
        foreach (var data in deserializedJsonItemConsumableSupply)
        {
            _itemConsumableSupplyDB.Add(data.ID, data);
        }

        string jsonItemEquipmentWeapon =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Item\itemDataConfig_equipment_weapon.json");
        List<ItemDataEquipmentWeapon> deserializedJsonItemEquipmentWeapon =
            JsonConvert.DeserializeObject<List<ItemDataEquipmentWeapon>>(jsonItemEquipmentWeapon);
        _itemEquipmentWeaponDB = new Dictionary<int, ItemDataEquipmentWeapon>();
        foreach (var data in deserializedJsonItemEquipmentWeapon)
        {
            _itemEquipmentWeaponDB.Add(data.ID, data);
        }

        string jsonBuffIconPath =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Buff\buffIconPathConfig.json");
        List<DeserializedJsonBuff> deserializedJsonBuffs =
            JsonConvert.DeserializeObject<List<DeserializedJsonBuff>>(jsonBuffIconPath);
        _buffIconPathDB = new Dictionary<EBuffName, string>();

        foreach (var data in deserializedJsonBuffs)
        {
            _buffIconPathDB.Add(data.Name, data.SpritePath);
        }
    }

    /// <summary>
    /// A helper class for converting json data of buff to a dictionary
    /// </summary>
    private class DeserializedJsonBuff
    {
        public int ID;
        public EBuffName Name;
        public string SpritePath;
    }

    #region Debug Helper

    private void DebugDict()
    {
        string content = "";
        foreach (var item in _itemEquipmentWeaponDB)
        {
            content += $"{item.Key} : {item.Value.Name}\n";
        }

        Debug.Log(content);
    }

    #endregion
}