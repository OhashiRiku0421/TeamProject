using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameSystem : MonoBehaviour, IPause
{
    [SerializeField]
    private TimerSystem _timerSystem = new();

    [SerializeField]
    private Transform[] _starts;

    private ScoreSystem _scoreSystem = new();

    private bool _isPause = false;

    private Transform _playerTransform;

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
        _playerTransform = FindObjectOfType<PlayerHPController>().gameObject.transform;
        GameStart();
        _timerSystem.Start();
        CriAudioManager.Instance.BGM.Play("BGM", "BGM_Ingame_01");
    }

    private void Update()
    {
        if (!_isPause)
        {
            _timerSystem.Update();
        }
    }

    private void GameStart()
    {
        int index = Random.Range(0, _starts.Length);
        Debug.Log(index);
        _playerTransform.position = _starts[index].position;
    }
}
