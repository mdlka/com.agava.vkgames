using System;
using UnityEngine.Scripting;

namespace Agava.VKGames.Samples.Playtesting
{
    [Serializable]
    public class PurchasedItemsSaveData
    {
        [field: Preserve] public string[] Items;
    }
}