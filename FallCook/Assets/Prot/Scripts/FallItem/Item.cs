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

        [SerializeField]
        ItemConstant.Type Type;

        Image _image;

        string _destroyTag;

        string _playerTag;

        readonly Subject < (string tKey, int tScore) > _collisionSubject = new Subject < (string tKey, int tScore) > ();
        public IObservable < (string tKey, int tScore) > CollisionIObservable => _collisionSubject;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag == "Destroy")
            {
                Destroy(this.gameObject);
            }
            else if (other.gameObject.tag == "Player")
            {
                _collisionSubject.OnNext((Key, Score));
                Destroy(this.gameObject);
            }
        }

        ItemConstant.Type GetItemType()
        {
            return Type;
        }
    }
}
