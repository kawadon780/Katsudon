using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class Player : MonoBehaviour
    {

        Image _image;

        public Transform UpdateTransform { get; set; }

        void Awake()
        {
            Init();
        }

        void Init()
        {
            UpdateTransform = this.transform;
        }
    }
}
