using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour, IPause
{
    [SerializeField]
    private TimerSystem _timerSystem = new();

    private ScoreSystem _scoreSystem = new();

    private bool _isPause = false;

    public TimerSystem TimerSystem => _timerSystem;

    public ScoreSystem ScoreSystem => _scoreSystem;

    public void Pause()
    {
        _isPause = true;
    }

    public void Resume()
    {
        _isPause = false;
    }

    private void Start()
    {
        _timerSystem.Start();
    }

    private void Update()
    {
        _timerSystem.Update();
    }
}
