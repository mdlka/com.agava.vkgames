using System.Collections.Generic;
using UnityEngine;

namespace Agava.VKGames.Samples.Playtesting
{
    public class ItemCatalogPanel : MonoBehaviour
    {
        [SerializeField] private ItemCatalog _catalog;
        [SerializeField] private PurchasedItemListPanel _purchasedItemListPanel;
        [SerializeField] private ItemPanel _itemTemplate;
        [SerializeField] private Transform _content;

        private List<ItemPanel> _itemInstances;
        
        private void Awake()
        {
            _itemInstances = new List<ItemPanel>();

            foreach (var item in _catalog.Items)
            {
                var itemInstance = Instantiate(_itemTemplate, _content);
                itemInstance.Initialize(item);
                itemInstance.ButtonClicked += OnItemButtonClicked;
                
                _itemInstances.Add(itemInstance);
            }
        }

        private void OnDestroy()
        {
            foreach (var item in _itemInstances)
                item.ButtonClicked -= OnItemButtonClicked;
        }
        
        private void OnItemButtonClicked(ItemPanel itemPanel)
        {
            Billing.PurchaseItem(itemPanel.Item.item_id, onSuccessCallback: () =>
            {
                _purchasedItemListPanel.Add(itemPanel.Item);
                Debug.Log($"Purchased {itemPanel.Item.item_id}");
            });
        }
    }
}
