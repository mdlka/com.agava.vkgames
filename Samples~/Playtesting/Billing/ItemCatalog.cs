using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Agava.VKGames.Samples.Playtesting
{
    [CreateAssetMenu(menuName = "VkGames/Samples/Create ItemCatalog", fileName = "ItemCatalog", order = 56)]
    public class ItemCatalog : ScriptableObject
    {
        [SerializeField] private Item[] _items;
        
        public IEnumerable<Item> Items => _items;

        [ContextMenu("Convert To Json")]
        private void CreateJsonFile()
        {
            Debug.Log(JsonConvert.SerializeObject(_items, Formatting.Indented));
        }
    }
}