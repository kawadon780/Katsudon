using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Kawado.Item
{
    public class ItemManager : MonoBehaviour
    {
        [SerializeField]
        Transform _field;

        [SerializeField]
        ItemList _items;
        Dictionary<string, Item> _fallItems;

        [SerializeField]
        Transform _defaultPosition;

        const string ItemName = "fallitem";

        int itemIndex;

        readonly Subject < (ItemSetting.ItemType itemType, string tKey, int tScore) > _collisionSubject = new Subject < (ItemSetting.ItemType itemType, string tKey, int tScore) > ();
        public IObservable < (ItemSetting.ItemType itemType, string tKey, int tScore) > CollisionIObservable => _collisionSubject;

        void Awake()
        {
            _fallItems = new Dictionary<string, Item>();
            var items = _items.GetItemList();
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(collisionItem =>
            {
                itemIndex++;
                var instanceItem = Instantiate(items[GetRandomIndex(items.Length)], new Vector2(_defaultPosition.position.x + UnityEngine.Random.Range(-340.0f, 340.0f), _defaultPosition.position.y), Quaternion.identity, _field);
                instanceItem.Key = ItemName + itemIndex.ToString();
                instanceItem.gameObject.SetActive(true);
                _fallItems.Add(instanceItem.Key, instanceItem);
                UpdateColisionItem(instanceItem);
            }).AddTo(this);
        }

        int GetRandomIndex(int max)
        {
            return UnityEngine.Random.Range(0, max);
        }

        void UpdateColisionItem(Item item)
        {
            item.CollisionIObservable.Subscribe(colisionItem =>
            {
                _collisionSubject.OnNext((colisionItem.itemType, colisionItem.tKey, colisionItem.tScore));
            }).AddTo(this);
        }

    }
}
