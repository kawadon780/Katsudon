using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class PlayerData : MonoBehaviour
    {
        [field : SerializeField]
        public Image SetImage { get; set; }

        [field : SerializeField]
        public PlayerSetting.CookedStatus CookStatus { get; set; }
    }
}
