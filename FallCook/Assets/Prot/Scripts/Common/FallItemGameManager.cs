using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UniRx;
using Kawado.Item;
using Kawado.Player;
using Kawado.Result;

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

        int _time = 3000;

        int GameEndTime = 0;

        public bool IsGameEnd { get; private set; }

        [SerializeField]
        GameStop _gameStop;

        [SerializeField]
        ResultDisplay _resultDisplay;

        bool OnPause;

        Dictionary<PlayerConstant.Status, int> _point { get; set; } =
        new Dictionary<PlayerConstant.Status, int>()
        { { PlayerConstant.Status.Katsu, 0 }, { PlayerConstant.Status.Rice, 0 }, { PlayerConstant.Status.Ricekatsu, 100 }, { PlayerConstant.Status.Katsudon, 300 }, { PlayerConstant.Status.KatsuKatsu, 150 }, { PlayerConstant.Status.RiceRice, 50 }
        };

        Dictionary<PlayerConstant.Status, int> _itemNum;

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
            if (IsGameEnd)
            {
                return;
            }

            TimeSet();
        }

        void TimeSet()
        {
            if (!OnPause)
            {
                return;
            }

            if (GameEndTime >= _time)
            {
                _playerManager.StopColision();
                _itemManager.FallStop();
                const string mark = " X ";
                IsGameEnd = true;
                _resultDisplay.OnDisplay(true);

                _itemNum = _playerManager.GetItem;
                _resultDisplay.Katudon.text = mark + _itemNum[PlayerConstant.Status.Katsudon].ToString();
                _resultDisplay.RiceKatsu.text = mark + _itemNum[PlayerConstant.Status.Ricekatsu].ToString();
                _resultDisplay.RiceRice.text = mark + _itemNum[PlayerConstant.Status.RiceRice].ToString();
                _resultDisplay.KatsuKatsu.text = mark + _itemNum[PlayerConstant.Status.KatsuKatsu].ToString();
                _resultDisplay.RiceRice.text = mark + _itemNum[PlayerConstant.Status.RiceRice].ToString();
                _resultDisplay.Score.text = GetScore().ToString();
                return;
            }

            _time = _time - 2;
            _timeDisplay.text = _time.ToString();
        }

        void ScoreUpdate(Kawado.Item.Item instanceItem)
        {
            instanceItem.CollisionIObservable.Subscribe(colisionItem =>
            {
                _scoreDisplay.SetScore(GetScore());
                _playerManager.UpdateStatus(instanceItem.Type);
            }).AddTo(this);
        }

        public void MoveTitle()
        {
            SceneManager.LoadScene("Title");
        }

        public void MoveGame()
        {
            SceneManager.LoadScene("Game");
        }

        int GetScore()
        {
            _itemNum = _playerManager.GetItem;
            var score =
                (_itemNum[PlayerConstant.Status.Katsudon] * _point[PlayerConstant.Status.Katsudon]) +
                (_itemNum[PlayerConstant.Status.Ricekatsu] * _point[PlayerConstant.Status.Ricekatsu]) +
                (_itemNum[PlayerConstant.Status.KatsuKatsu] * _point[PlayerConstant.Status.KatsuKatsu]) +
                (_itemNum[PlayerConstant.Status.RiceRice] * _point[PlayerConstant.Status.RiceRice]);
            return score;
        }
    }
}
