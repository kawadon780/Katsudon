using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Kawado.Item;

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

        // 補間の強さ（0f～1f） 。0なら追従しない。1なら遅れなしに追従する。
        [SerializeField, Range(0f, 1f)] private float followStrength;

        Vector3 targetPos;

        bool onPause;

        PlayerConstant.Status _status;

        [SerializeField]
        Image[] _imageList;

        [SerializeField]
        Rigidbody2D _rigidbody2D;

        public bool IsGameEnd { get; set; }

        public Dictionary<PlayerConstant.Status, int> GetItem { get; private set; } =
            new Dictionary<PlayerConstant.Status, int>()
            { { PlayerConstant.Status.Katsu, 0 }, { PlayerConstant.Status.Rice, 0 }, { PlayerConstant.Status.Ricekatsu, 0 }, { PlayerConstant.Status.Katsudon, 0 }, { PlayerConstant.Status.KatsuKatsu, 0 }, { PlayerConstant.Status.RiceRice, 0 }
            };

        void Update()
        {
            if (onPause) return;

            targetPos = Input.mousePosition;

            // X, Y座標の範囲を制限する
            targetPos.y = Mathf.Clamp(targetPos.y, bounds.yMin, bounds.yMax);
            targetPos.z = 0f;

            // このスクリプトがアタッチされたゲームオブジェクトを、マウス位置に線形補間で追従させる
            _player.UpdateTransform.position = Vector3.Lerp(transform.position, targetPos, followStrength);
        }

        public void UpdateStatus(ItemConstant.Type item)
        {
            switch (_status)
            {

            case PlayerConstant.Status.Empty:

                if (item == ItemConstant.Type.Rice)
                {
                    _status = PlayerConstant.Status.Rice;
                    _player.NowImage.sprite = _imageList[2].sprite;
                    break;
                }

                if (item == ItemConstant.Type.Katsu)
                {
                    _status = PlayerConstant.Status.Katsu;
                    _player.NowImage.sprite = _imageList[1].sprite;
                }
                break;

            case PlayerConstant.Status.Katsu:

                if (item == ItemConstant.Type.Rice)
                {
                    _status = PlayerConstant.Status.Ricekatsu;
                }

                if (item == ItemConstant.Type.Katsu)
                {
                    _status = PlayerConstant.Status.KatsuKatsu;
                }
                break;

            case PlayerConstant.Status.Rice:

                if (item == ItemConstant.Type.Rice)
                {
                    _status = PlayerConstant.Status.RiceRice;
                }

                if (item == ItemConstant.Type.Katsu)
                {
                    _status = PlayerConstant.Status.Katsudon;
                }
                break;

            default:
                break;

            }
            GetItem[_status]++;
            Debug.Log(_status);

            if (PlayerConstant.Status.Katsu == _status || PlayerConstant.Status.Rice == _status)
            {
                return;
            }

            _status = PlayerConstant.Status.Empty;
            _player.NowImage.sprite = _imageList[0].sprite;
        }

        public void StopColision()
        {
            _rigidbody2D.simulated = false;
        }
    }
}
