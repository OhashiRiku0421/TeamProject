using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[System.Serializable]
public class TimerSystem
{
    [SerializeField]
    private SceneSwitcher _sceneSwitcher;

    public static float Timer = 1200;

    private Subject<Unit> _gameOver = new();

    public void Start()
    {
        _gameOver.Take(1).Subscribe(_ => _sceneSwitcher.SceneSwitch());
    }

    public void Update()
    {
        if(Timer <= 0)
        {
            _gameOver.OnNext(Unit.Default);
        }
        else
        {
            Timer -= Time.deltaTime;
        }
    }

}
