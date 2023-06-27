using System.Collections.Generic;
using UnityEngine;

namespace Agava.VKGames.Samples.Playtesting
{
    public class PurchasedItemListPanel : MonoBehaviour
    {
        private readonly List<ItemPanel> _itemInstances = new List<ItemPanel>();
        private readonly List<Item> _purchasedItems = new List<Item>();
        
        [SerializeField] private ItemPanel _itemTemplate;
        [SerializeField] private Transform _content;

        private void OnEnable()
        {
            foreach (var item in _purchasedItems)
            {
                var itemInstance = Instantiate(_itemTemplate, _content);
                itemInstance.Initialize(item);
                itemInstance.ButtonClicked += OnItemButtonClicked;
                
                _itemInstances.Add(itemInstance);
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
            _purchasedItems.Add(item);
        } 
        
        private void OnItemButtonClicked(ItemPanel itemPanel)
        {
            if (_purchasedItems.Contains(itemPanel.Item))
                _purchasedItems.Remove(itemPanel.Item);

            if (_itemInstances.Contains(itemPanel))
                _itemInstances.Remove(itemPanel);
            
            Destroy(itemPanel.gameObject);
            
            Debug.Log($"Consumed {itemPanel.Item.item_id}");
        }
    }
}