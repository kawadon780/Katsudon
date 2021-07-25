using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class Player : MonoBehaviour
    {

        [field : SerializeField]
        public PlayerSetting.CookedStatus CookedStatus { private set; get; }
        public Transform UpdateTransform { get; set; }

        [SerializeField]
        Image _myDisplay;

        void Awake()
        {

        public void SetStatus(PlayerSetting.CookedStatus status, Image image)
        {
            CookedStatus = status;
            _myDisplay.sprite = image.sprite;
        }
    }sasasa[sa]
}
