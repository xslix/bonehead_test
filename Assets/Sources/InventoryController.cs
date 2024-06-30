using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using Zenject;
using Random = UnityEngine.Random;

namespace Sources
{
    
    //с таким подходом по хорошему класс не должен быть монобехом, а частью некоего ядра,
    //но в рамках тестового проекта сделал его просто монобехом.
    //+должна идти его регистрация через интерфейс
    public class InventoryController : MonoBehaviour
    {
        public UnityEvent<int> coinsChangedEvent = new UnityEvent<int>();
        public UnityEvent itemsChangedEvent = new UnityEvent();
        
        [Inject] private ItemsData _itemsData;

       public int Coins
        {
            get => _coins;
            private set
            {
                _coins = value;
                coinsChangedEvent?.Invoke(_coins);
            }
        }
       
        private int _coins = 0;

        public List<Item> items { get; private set; } = new List<Item>();

        private void Start()
        {
            Coins = _itemsData.startMoney;
        }

        public Item TryBuyItem()
        {
            if (_coins > _itemsData.purchaseCost)
            {
                return BuyItem();
            }
            else return null;
        }

        public void SellItem()
        {
            Coins += _itemsData.purchaseCost;
        }

        public void GetItem(Item item)
        {
            var oldItem = items.FirstOrDefault(x => x.id == item.id);
            if (oldItem == null)
            {
                items.Add(item);
            }
            else
            {
                oldItem.level = item.level;
                oldItem.rarity = item.rarity;
            }
            itemsChangedEvent?.Invoke();
        }
        
        private Item BuyItem()
        {
            Coins -= _itemsData.purchaseCost;
            var item = GetRandomItem();
            return item;

        }

        private Item GetRandomItem()
        {
            var item = new Item();
            item.id = Random.Range(0, _itemsData.itemsInfo.Count);
            int level = 1;
            if (items.Count > 0)
            {
               level = Math.Max(items.Select(x => x.level).Min() + _itemsData.minLevelModifier, 1);
            }
        
            while (Random.Range(0f, 1f) < _itemsData.levelUpgradeChance)
            {
                level++;
            }

            item.level = level;
            item.rarity = 0;
            while (item.rarity < RarityType.Legendary &&
                   Random.Range(0f, 1f) < (_itemsData.rarityInfo.FirstOrDefault(x => x.rarity == item.rarity)?.upgradeChance ?? 0))
                item.rarity++;
            return item;
        }
        
    }
}