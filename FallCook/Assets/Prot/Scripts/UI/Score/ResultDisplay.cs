using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Result
{
    public class ResultDisplay : MonoBehaviour
    {
        [SerializeField]
        GameObject _display;

        [field : SerializeField]
        public Text Katudon { get; set; }

        [field : SerializeField]
        public Text RiceKatsu { get; set; }

        [field : SerializeField]
        public Text RiceRice { get; set; }

        [field : SerializeField]
        public Text KatsuKatsu { get; set; }

        [field : SerializeField]
        public Text Score { get; set; }

        public void OnDisplay(bool onEnable)
        {
            _display.SetActive(onEnable);
        }

    }
}
