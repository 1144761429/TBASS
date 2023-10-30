using System;
using System.Collections.Generic;
using System.IO;
using BuffSystem.Common;
using Characters.Enemies.SerializableData;
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

    private Dictionary<int, EnemyData> _enemyDB;

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

    public EnemyData GetEnemyData(int id)
    {
        if (_enemyDB.TryGetValue(id, out EnemyData enemyData))
        {
            return enemyData;
        }

        throw new Exception($"Retrieving data failed due to enemy id does not exist. ID: {id}");
    }

    public static bool IsValidWeaponID(int id)
    {
        if (id > 100000 && id < 1000000)
        {
            return true;
        }

        throw new ArgumentException("Invalid Weapon ID.\n" +
                                    $"ID: {id}");
    }
    
    public static bool IsValidEnemyID(int id)
    {
        if (id > 10000 && id < 100000)
        {
            return true;
        }

        throw new ArgumentException("Invalid Enemy ID.\n" +
                                    $"ID: {id}");
    }
    
    private void InitDBDict()
    {
        string jsonItemConsumableSupply =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Item\ItemDataConfig_Consumable_Supply.json");
        List<ItemDataConsumableSupply> deserializedJsonItemConsumableSupply =
            JsonConvert.DeserializeObject<List<ItemDataConsumableSupply>>(jsonItemConsumableSupply);
        _itemConsumableSupplyDB = new Dictionary<int, ItemDataConsumableSupply>();
        foreach (var data in deserializedJsonItemConsumableSupply)
        {
            _itemConsumableSupplyDB.Add(data.ID, data);
        }

        string jsonItemEquipmentWeapon =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Item\ItemDataConfig_Equipment_Weapon.json");
        List<ItemDataEquipmentWeapon> deserializedJsonItemEquipmentWeapon =
            JsonConvert.DeserializeObject<List<ItemDataEquipmentWeapon>>(jsonItemEquipmentWeapon);
        _itemEquipmentWeaponDB = new Dictionary<int, ItemDataEquipmentWeapon>();
        foreach (var data in deserializedJsonItemEquipmentWeapon)
        {
            _itemEquipmentWeaponDB.Add(data.ID, data);
        }

        string jsonBuffIcon =
            File.ReadAllText(
                @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Buff\BuffIconPathConfig.json");
        List<DeserializedJsonBuff> deserializedJsonBuffs =
            JsonConvert.DeserializeObject<List<DeserializedJsonBuff>>(jsonBuffIcon);
        _buffIconPathDB = new Dictionary<EBuffName, string>();

        foreach (var data in deserializedJsonBuffs)
        {
            _buffIconPathDB.Add(data.Name, data.SpritePath);
        }
        
        string jsonEnemyData = 
            File.ReadAllText(
                    @"C:\Users\11447\Desktop\GameDev_CS\Unity Project\TBASS\Assets\JsonData\Enemy\EnemyDataConfig.json");
        List<EnemyData> deserializedEnemyData = JsonConvert.DeserializeObject<List<EnemyData>>(jsonEnemyData);
        _enemyDB = new Dictionary<int, EnemyData>();

        foreach (var data in deserializedEnemyData)
        {
            _enemyDB.Add(data.ID, data);
        }
    }
    
    
    //TODO: put this to the buff directory
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