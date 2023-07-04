using System;
using UnityEngine.Scripting;

namespace Agava.VKGames
{
    [Serializable]
    internal class StringKeyValuePair
    {
        [field: Preserve] public string key;
        [field: Preserve] public string value;
    }
}