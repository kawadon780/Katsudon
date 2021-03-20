using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        [SerializeField] Bounds bounds;

        PlayerSetting.Status CookStatus;

        // 補間の強さ（0f～1f） 。0なら追従しない。1なら遅れなしに追従する。
        [SerializeField, Range(0f, 1f)] private float followStrength;

        Vector3 targetPos;

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
            var ScoreKeisan = new Dictionary<PlayerSetting.Status, float>()
                { { PlayerSetting.Status.Empty, 1 }, { PlayerSetting.Status.Katsudon, 1 }, { PlayerSetting.Status.Katu, 1 }, { PlayerSetting.Status.Rice, 1 }, { PlayerSetting.Status.Donkatsu, 1 }
                };
        }
    }
}
