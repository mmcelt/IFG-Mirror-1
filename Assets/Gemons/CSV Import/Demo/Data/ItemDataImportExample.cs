using System;
using UnityEngine;

namespace Gemons.CsvImport.Demo
{
    public class ItemDataImportExample : CsvImportBase, IFormattable
    {
        #region Public Fields
        public int ID;
        public string DisplayName;
        public string DurabilityFunction;
        public float BaseValue;
        public float RepairCost;
        public float CraftingCost;
        public Sprite Image;
        #endregion

        #region Properties
        public string Type
        {
            get
            {
                return ItemType.ToString();
            }

            set
            {
                if (Enum.TryParse(value, out ItemType) == false)
                {
                    Debug.LogError($"Could not parse '{value}' to enum ItemType");
                    ItemType = ItemType.Undefined;
                }
            }
        }
        public ItemType ItemType;

        public string Rarity
        {
            get
            {
                return ItemRarity.ToString();
            }
            set
            {
                if (Enum.TryParse(value, out ItemRarity) == false)
                {
                    Debug.LogError($"Could not parse '{value}' to enum ItemRarity");
                    ItemRarity = ItemRarity.Common;
                }
            }
        }
        public ItemRarity ItemRarity;
        #endregion

        #region Methods
        public override string ToString(string format, IFormatProvider formatProvider)
        {
            return $"ID: {ID}, Name: {Name}, Rarity: {Rarity}";
        }

        /// <summary>
        /// Update all fields except the image reference which is not included in CSV file.
        /// </summary>
        /// <param name="importedData"></param>
        public override void UpdateFromImportedData(CsvImportBase importedData)
        {
            ItemDataImportExample importedItemData = (ItemDataImportExample)importedData;
            Name = importedItemData.Name;
            ID = importedItemData.ID;
            DisplayName = importedItemData.DisplayName;
            DurabilityFunction = importedItemData.DurabilityFunction;
            BaseValue = importedItemData.BaseValue;
            RepairCost = importedItemData.RepairCost;
            CraftingCost = importedItemData.CraftingCost;
            Type = importedItemData.Type;
            Rarity = importedItemData.Rarity;
        }
        #endregion

    }

    public enum ItemType
    {
        Undefined = 0,
        Armor = 1,
        Sword = 2,
        Bow = 3,
        Potions = 4,
        Cooking = 5,
        Tools = 6,
        Food = 7,
        Wood = 8,
        Cloths = 9,
    }

    public enum ItemRarity
    {
        Undefined = 0,
        Common = 1,
        Uncommon = 2,
        Rare = 3,
        Epic = 4,
        Legendary = 5,
    }
}

