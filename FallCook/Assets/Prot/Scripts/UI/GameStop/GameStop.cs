using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

namespace Kawado.Common
{
    public class GameStop : MonoBehaviour
    {

        [SerializeField]
        GameObject StopPanel;

        [SerializeField]
        Text timer;

        IObservable<int> _countIObservable;

        const int Count = 3;

        Subject<Unit> _counterSubject = new Subject<Unit>();
        public IObservable<Unit> CounterIObservable => _counterSubject;

        void Start()
        {
            OnPanel(true);
        }

        public void CountStart()
        {
            _countIObservable = CreateCountDownObservable(Count).TakeUntilDestroy(this);

            _countIObservable.Subscribe(_ =>
            {
                timer.text = _.ToString();
            }, () =>
            {
                // OnComplete
                _counterSubject.OnNext(Unit.Default);
            });

        }

        public void OnPanel(bool OnEnable)
        {
            StopPanel.SetActive(OnEnable);
        }

        private IObservable<int> CreateCountDownObservable(int CountTime)
        {
            return Observable
                .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) //0秒後から1秒間隔で実行
                .Select(x => (int) (CountTime - x)) //xは起動してからの秒数
                .TakeWhile(x => x > 0); //0秒超過の間はOnNext、0になったらOnComplete
        }
    }
}
