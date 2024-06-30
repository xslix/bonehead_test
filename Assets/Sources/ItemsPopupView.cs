using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Zenject;


namespace Sources
{
    public class ItemsPopupView : MonoBehaviour
    {
        [SerializeField] private Button buttonBuy;
        [SerializeField] private ItemRenderer itemPrefab;
        [SerializeField] private Transform itemsGroupTransform;
        [SerializeField] private TextMeshProUGUI coinsText;
        [SerializeField] public GetItemPopupView getItemPopup;

        [Inject] private InventoryController _inventoryController;
        [Inject] private ItemsData _itemsData;
        
        
        public UnityEvent buyItemEvent = new UnityEvent(); 
        private List<ItemRenderer> _items = new List<ItemRenderer>();
        private Item _purchase;

        private void Start()
        {
            buttonBuy.onClick.AddListener(OnButtonBuy);
            getItemPopup.buttonGet.onClick.AddListener(OnButtonGet);
            getItemPopup.buttonSell.onClick.AddListener(OnButtonSell);
            _inventoryController.coinsChangedEvent.AddListener(OnCoinsChanged);
            _inventoryController.itemsChangedEvent.AddListener(OnItemsChanged);
            OnCoinsChanged(_inventoryController.Coins);
        }

        private void OnDestroy()
        {
            buttonBuy.onClick.RemoveAllListeners();
            getItemPopup.buttonGet.onClick.RemoveAllListeners();
            getItemPopup.buttonSell.onClick.RemoveAllListeners();
            _inventoryController.coinsChangedEvent.RemoveListener(OnCoinsChanged);
            _inventoryController.itemsChangedEvent.RemoveListener(OnItemsChanged);
        }

        private void OnButtonBuy()
        {
            _purchase = _inventoryController.TryBuyItem();
            if (_purchase == null)
            {
                return;
            }
            var oldItem = _inventoryController.items.FirstOrDefault(x => x.id == _purchase.id);
            getItemPopup.Enable(_purchase, oldItem);
        }

        private void OnButtonSell()
        {
            getItemPopup.Disable();
            _inventoryController.SellItem();
        }

        private void OnButtonGet()
        {
            getItemPopup.Disable();
            _inventoryController.GetItem(_purchase);
        }

        private void OnCoinsChanged(int value)
        {
            coinsText.text = value.ToString();
        }

        private void OnItemsChanged()
        {
            while (_inventoryController.items.Count > _items.Count)
            {
                _items.Add(Instantiate(itemPrefab, itemsGroupTransform));
            }

            for (int i = 0; i < _items.Count; ++i)
            {
                _items[i].SetItem(_inventoryController.items[i], _itemsData);
            }
        }
    }
}