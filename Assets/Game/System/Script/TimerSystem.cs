using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class TimerSystem
{
    [SerializeField, Tooltip("タイマーの初期値")]
    private float _maxTime = 60;

    private ReactiveProperty<float> _timer = new();

    public IReactiveProperty<float> Timer => _timer;

    public void Start()
    {
        _timer.Value = _maxTime;
        _timer.FirstOrDefault(x => x <= 0f).Subscribe(_ => Debug.Log("GameOver"));
    }

    public void Update()
    {
        _timer.Value -= Time.deltaTime;
    }

}
