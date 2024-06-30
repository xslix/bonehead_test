using System;
using System.Collections.Generic;
using UnityEngine;

namespace Sources
{
    [CreateAssetMenu(fileName = "ItemsData", menuName = "ScriptableObjects/ItemsData", order = 1)]
    public class ItemsData : ScriptableObject
    {
        public float levelUpgradeChance;
        public int minLevelModifier;
        public int purchaseCost;
        public int startMoney;
        public List<RarityInfo> rarityInfo;
        public List<ItemInfo> itemsInfo;
    }

    public enum RarityType
    {
        Usual = 0,
        Unusual = 1,
        Rare = 2,
        Mythcal = 3,
        Legendary = 4
    }

    [Serializable]
    public class RarityInfo
    {
        public RarityType rarity;
        public Color color;
        public float upgradeChance;
    }
    
    [Serializable]
    public class ItemInfo
    {
        public int id;
        public Sprite sprite;
    }
}