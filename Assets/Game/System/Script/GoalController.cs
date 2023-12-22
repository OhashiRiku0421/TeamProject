using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    private InGameSystem _inGameSystem;
    private SceneSwitcher _sceneSwitcher;
    private void Start()
    {
        _inGameSystem = FindObjectOfType<InGameSystem>();
        _sceneSwitcher = _inGameSystem.GetComponent<SceneSwitcher>();
        if (_inGameSystem == null)
        {
            Debug.LogError("InGameSystemがないです");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            _inGameSystem.ScoreSystem.GameResult(TimerSystem.Timer);
            _sceneSwitcher.SceneSwitch();
        }
    }
}
