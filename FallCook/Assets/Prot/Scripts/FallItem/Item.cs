using System;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Kawado.Item
{
    public class Item : MonoBehaviour
    {
        string _key;

        int _score;

        Image _image;

        [SerializeField]
        string _destroyTag;

        [SerializeField]
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
                _collisionSubject.OnNext((_key, _score));
                Destroy(this.gameObject);
            }
        }

        public void SetValue(string key, int score, Image image)
        {
            _key = key;
            _score = score;
            //_image = image;
        }

    }
}
