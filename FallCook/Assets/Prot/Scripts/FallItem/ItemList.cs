using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Kawado.Item
{
    public class ItemList : MonoBehaviour
    {
        [SerializeField]
        Item[] _items;

        public Item[] GetItemList()
        {
            return _items;
        }
    }
}
