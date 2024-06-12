using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (SceneManager.GetActiveScene().name == "GroundStage_masterup")
        {
            CriAudioManager.Instance.BGM.Play("BGM", "BGM_Ingame_01");
        }
        else
        {
            CriAudioManager.Instance.BGM.Play("BGM", "BGM_Ingame_02");
        }
        StartCoroutine(PlayVoice());
    }

    IEnumerator PlayVoice()
    {
        yield return new WaitForSeconds(1); 
        CriAudioManager.Instance.SE.Play("VOICE", "VC_Robe_Ingame_Start");
        yield return new WaitForSeconds(2.3f);
        CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Ingame_Start");
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
        _playerTransform.position = _starts[index].position;
    }
}
