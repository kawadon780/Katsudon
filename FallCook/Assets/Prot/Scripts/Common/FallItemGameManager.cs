using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Kawado.Item;
using Kawado.Player;

namespace Kawado.Common
{
    public class FallItemGameManager : SingletonMonoBehaviour<FallItemGameManager>
    {
        [SerializeField]
        ItemManager _itemManager;

        [SerializeField]
        PlayerManager _playerManager;

        [SerializeField]
        Text _timeDisplay;

        [SerializeField]
        ScoreDisplay _scoreDisplay;

        int time;

        int GameEndTime = 3000;

        public bool IsGameEnd { get; private set; }

        [SerializeField]
        GameStop _gameStop;

        bool OnPause;

        void Awake()
        {
            Application.targetFrameRate = 60;
            _gameStop.CountStart();

            _gameStop.CounterIObservable.Subscribe(_ =>
            {
                _gameStop.OnPanel(false);
                _itemManager.ItemGaneration();
                OnPause = true;
            });

            _itemManager.CollisionIObservable.Subscribe(item =>
            {
                ScoreUpdate(item);
            }).AddTo(this);

        }

        void Update()
        {
            TimeSet();
        }

        void TimeSet()
        {
            if (!OnPause)
            {
                return;
            }

            if (GameEndTime == time)
            {
                IsGameEnd = true;
            }

            time++;
            _timeDisplay.text = time.ToString();
        }

        void ScoreUpdate(Kawado.Item.Item instanceItem)
        {
            instanceItem.CollisionIObservable.Subscribe(colisionItem =>
            {
                _scoreDisplay.Addition(colisionItem.tScore);
            }).AddTo(this);
        }
    }
}
