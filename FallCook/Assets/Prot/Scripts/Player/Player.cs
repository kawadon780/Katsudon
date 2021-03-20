using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        Image[] _image;

        [SerializeField]
        PlayerSetting.Status CookStatus;
        public Transform UpdateTransform { get; set; }

        void Awake()
        {
            Init();
        }

        void Init()
        {
            UpdateTransform = this.transform;
        }

        Se
    }
}
