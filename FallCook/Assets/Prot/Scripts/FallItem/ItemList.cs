using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kawado.Player;
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
