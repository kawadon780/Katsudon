using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Kawado.Item
{
    public class Item : MonoBehaviour
    {
        [field : SerializeField]
        public string Key { get; set; }

        [field : SerializeField]
        public int Score { get; private set; }

        Image _image;

        [SerializeField]
        string _destroyTag;

        [SerializeField]
        string _playerTag;

        [SerializeField]
        ItemSetting.ItemType _itemType;

        readonly Subject < (ItemSetting.ItemType itemType, string tKey, int tScore) > _collisionSubject = new Subject < (ItemSetting.ItemType itemType, string tKey, int tScore) > ();
        public IObservable < (ItemSetting.ItemType itemType, string tKey, int tScore) > CollisionIObservable => _collisionSubject;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Destroy")
            {
                Destroy(this.gameObject);
            }
            else if (other.gameObject.tag == "Player")
            {
                _collisionSubject.OnNext((_itemType, Key, Score));
                Destroy(this.gameObject);
            }
        }
    }
}
