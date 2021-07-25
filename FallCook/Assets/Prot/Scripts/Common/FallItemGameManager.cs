using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Kawado.Item;
using Kawado.Player;
using Kawado.Score;

namespace Kawado.Common
{
    public class FallItemGameManager : SingletonMonoBehaviour<FallItemGameManager>
    {
        [SerializeField]
        ItemManager _itemManager;

        [SerializeField]
        PlayerManager _playerManager;

        [SerializeField]
        ScoreDisplay _scoreDisplay;

        void Awake()
        {

            _itemManager.CollisionIObservable.Subscribe(colisionItem =>
            {
                _scoreDisplay.Addition(colisionItem.tScore);
                _playerManager.SetPlayerStatus(GetCookedStatus(colisionItem.itemType));

            }).AddTo(this);
        }

        PlayerSetting.CookedStatus _nextStatus = PlayerSetting.CookedStatus.Empty;
        PlayerSetting.CookedStatus GetCookedStatus(ItemSetting.ItemType itemType)
        {
            if (_playerManager.CookStatus == PlayerSetting.CookedStatus.Empty)
            {
                if (ItemSetting.ItemType.katsu == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Katu;
                }
                else if (ItemSetting.ItemType.rice == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Rice;
                }
            }

            if (_playerManager.CookStatus == PlayerSetting.CookedStatus.Rice)
            {
                if (ItemSetting.ItemType.katsu == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Empty;
                }
                else if (ItemSetting.ItemType.rice == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Empty;
                }
            }

            if (_playerManager.CookStatus == PlayerSetting.CookedStatus.Katu)
            {
                if (ItemSetting.ItemType.katsu == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Empty;
                }
                else if (ItemSetting.ItemType.rice == itemType)
                {
                    _nextStatus = PlayerSetting.CookedStatus.Empty;
                }
            }

            // if (_playerManager.CookStatus == PlayerSetting.CookedStatus.)
            // {
            //     if (ItemSetting.ItemType.katsu == itemType)
            //     {
            //         _nextStatus = PlayerSetting.CookedStatus;
            //     }
            //     else if (ItemSetting.ItemType.rice == itemType)
            //     {
            //         _nextStatus = PlayerSetting.CookedStatus;
            //     }
            //     return _nextStatus;
            // }

            return _nextStatus;
        }
    }
}
