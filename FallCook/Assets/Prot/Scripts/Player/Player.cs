using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class Player : MonoBehaviour
    {

        [field : SerializeField]
        public Image NowImage { get; set; }

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
