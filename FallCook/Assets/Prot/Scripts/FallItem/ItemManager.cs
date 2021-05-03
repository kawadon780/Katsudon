using System;
using System.Collections.Generic;
using UnityEngine;
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

        [SerializeField]
        ScoreDisplay _scoreDisplay;

        readonly Subject<Item> _collisionSubject = new Subject<Item>();
        public IObservable<Item> CollisionIObservable => _collisionSubject;

        void Awake()
        {
            _fallItems = new Dictionary<string, Item>();
        }

        public void ItemGaneration()
        {
            var items = _items.GetItemList();
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                itemIndex++;
                var instanceItem = Instantiate(items[GetRandomIndex(items.Length)], new Vector2(_defaultPosition.position.x + UnityEngine.Random.Range(-340.0f, 340.0f), _defaultPosition.position.y), Quaternion.identity, _field);
                instanceItem.Key = ItemName + itemIndex.ToString();
                _collisionSubject.OnNext(instanceItem);
                instanceItem.gameObject.SetActive(true);
                _fallItems.Add(instanceItem.Key, instanceItem);
            }).AddTo(this);
        }

        int GetRandomIndex(int max)
        {
            return UnityEngine.Random.Range(0, max);
        }
    }
}
