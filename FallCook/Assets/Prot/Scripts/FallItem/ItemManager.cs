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
        Item _item;

        [SerializeField]
        Transform _field;
        Dictionary<string, Item> _fallItems;

        Image _image;

        const string ItemName = "fallitem";
        int itemIndex;
        void Awake()
        {
            _fallItems = new Dictionary<string, Item>();
            Observable.Interval(TimeSpan.FromSeconds(1)).Subscribe(_ =>
            {
                itemIndex++;
                var instanceItem = Instantiate(_item, new Vector2(_item.transform.position.x + UnityEngine.Random.Range(-340.0f, 340.0f), _item.transform.position.y), Quaternion.identity, _field);
                instanceItem.SetValue(itemIndex.ToString(), 100, _image);
                instanceItem.gameObject.SetActive(true);
                _fallItems.Add(itemIndex.ToString(), instanceItem);
            }).AddTo(this);
        }
    }
}
