using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;

public class TimeCounter : MonoBehaviour
{
    [SerializeField]
    Text timer;
    // Start is called before the first frame update

    IObservable<int> _CountIObservable;

    void Awake()
    {
        _CountIObservable = CreateCountDownObservable(3).TakeUntilDestroy(this);

        _CountIObservable.Subscribe(_ =>
        {
            timer.text = _.ToString();
        }, () =>
        {

        });
    }

    private IObservable<int> CreateCountDownObservable(int CountTime)
    {
        return Observable
            .Timer(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1)) //0秒後から1秒間隔で実行
            .Select(x => (int) (CountTime - x)) //xは起動してからの秒数
            .TakeWhile(x => x > 0); //0秒超過の間はOnNext、0になったらOnComplete
    }
}
