using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Kawado.Player
{
    public class PlayerManager : MonoBehaviour
    {

        [SerializeField]
        Player _player;

        [Serializable]
        public class Bounds
        {
            public float xMin, xMax, yMin, yMax;
        }

        [SerializeField]
        PlayerData[] _playerDataList;

        [SerializeField] Bounds bounds;

        // 補間の強さ（0f～1f） 。0なら追従しない。1なら遅れなしに追従する。
        [SerializeField, Range(0f, 1f)] private float followStrength;

        Vector3 targetPos;

        public PlayerSetting.CookedStatus CookStatus
        {
            private set
            {
                CookStatus = value;
            }
            get
            {
                return _player.CookedStatus;
            }
        }

        public Dictionary<PlayerSetting.CookedStatus, Image> statusImages { get; private set; } = new Dictionary<PlayerSetting.CookedStatus, Image>();

        void Update()
        {
            targetPos = Input.mousePosition;

            // X, Y座標の範囲を制限する
            targetPos.y = Mathf.Clamp(targetPos.y, bounds.yMin, bounds.yMax);
            targetPos.z = 0f;

            // このスクリプトがアタッチされたゲームオブジェクトを、マウス位置に線形補間で追従させる
            _player.UpdateTransform.position = Vector3.Lerp(transform.position, targetPos, followStrength);
        }

        void Awake()
        {
            var ScoreKeisan = new Dictionary<PlayerSetting.CookedStatus, float>()
                { { PlayerSetting.CookedStatus.Empty, 1 }, { PlayerSetting.CookedStatus.Katsudon, 1 }, { PlayerSetting.CookedStatus.Katu, 1 }, { PlayerSetting.CookedStatus.Rice, 1 }, { PlayerSetting.CookedStatus.Donkatsu, 1 }
                };

            foreach (var playerlist in _playerDataList)
            {
                statusImages.Add(playerlist.CookStatus, playerlist.SetImage);
                Debug.Log(playerlist.CookStatus);
            }

        }

        public void SetPlayerStatus(PlayerSetting.CookedStatus cookStatus)
        {
            _player.SetStatus(cookStatus, statusImages[cookStatus]);
        }
    }
}
