using System;
using UnityEngine.Scripting;

namespace Agava.VKGames
{
    [Serializable]
    internal class StorageValues
    {
        [field: Preserve] public StringKeyValuePair[] keys;
    }
}