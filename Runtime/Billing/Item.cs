using System;
using UnityEngine.Scripting;

namespace Agava.VKGames
{
    [Serializable]
    public class Item
    {
        [field: Preserve] public string ru_title;
        [field: Preserve] public string en_title;
        [field: Preserve] public string photo_url;
        [field: Preserve] public string item_id;
        [field: Preserve] public int price;
        [field: Preserve] public int discount;
        [field: Preserve] public int expiration;
    }
}