using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Agava.VKGames.Samples.Playtesting
{
    public class PurchasedItemListPanel : MonoBehaviour
    {
        private const string PurchasedItemSaveKey = "PurchasedItemSaveKey";
        
        private readonly List<ItemPanel> _itemInstances = new List<ItemPanel>();
        private readonly List<Item> _purchasedItems = new List<Item>();

        [SerializeField] private ItemCatalog _itemCatalog;
        [SerializeField] private ItemPanel _itemTemplate;
        [SerializeField] private Transform _content;
        
        private List<string> _savedItemsId;

        private void OnEnable()
        {
            if (_savedItemsId == null)
                LoadPurchasedItems(onLoaded: CreateItems);
            else
                CreateItems();
            
            void CreateItems()
            {
                foreach (var item in _purchasedItems)
                {
                    var itemInstance = Instantiate(_itemTemplate, _content);
                    itemInstance.Initialize(item);
                    itemInstance.ButtonClicked += OnItemButtonClicked;
                
                    _itemInstances.Add(itemInstance);
                }
            }
        }

        private void OnDisable()
        {
            foreach (var item in _itemInstances)
            {
                item.ButtonClicked -= OnItemButtonClicked;
                Destroy(item.gameObject);
            }
            
            _itemInstances.Clear();
        }

        public void Add(Item item)
        {
            if (_savedItemsId == null)
                LoadPurchasedItems(onLoaded: AddItem);
            else
                AddItem();

            void AddItem()
            {
                _purchasedItems.Add(item);
                _savedItemsId.Add(item.item_id);
                SavePurchasedItems();
            }
        }

        private void OnItemButtonClicked(ItemPanel itemPanel)
        {
            if (_purchasedItems.Contains(itemPanel.Item))
                _purchasedItems.Remove(itemPanel.Item);

            if (_itemInstances.Contains(itemPanel))
                _itemInstances.Remove(itemPanel);
            
            Destroy(itemPanel.gameObject);
            
            _savedItemsId.Remove(itemPanel.Item.item_id);
            SavePurchasedItems();

            Debug.Log($"Consumed {itemPanel.Item.item_id}");
        }

        private void LoadPurchasedItems(Action onLoaded = null)
        {
            _savedItemsId = new List<string>();
            
            Storage.GetCloudSaveData(PurchasedItemSaveKey, onSuccessCallback: value =>
            {
                _savedItemsId.AddRange(JsonUtility.FromJson<PurchasedItemsSaveData>(value).Items);
                _purchasedItems.AddRange(_savedItemsId.Select(itemId => 
                    _itemCatalog.Items.First(item => item.item_id == itemId)));
                
                onLoaded?.Invoke();
            }, onErrorCallback: () => onLoaded?.Invoke());
        }

        private void SavePurchasedItems()
        {
            Storage.SetCloudSaveData(PurchasedItemSaveKey, 
                JsonUtility.ToJson(new PurchasedItemsSaveData {Items = _savedItemsId.ToArray()}));
        }
    }
}