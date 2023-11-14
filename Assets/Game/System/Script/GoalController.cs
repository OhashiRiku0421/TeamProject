using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoalController : MonoBehaviour
{
    private InGameSystem _inGameSystem;

    private void Start()
    {
        _inGameSystem = FindObjectOfType<InGameSystem>();
        if(_inGameSystem == null)
        {
            Debug.LogError("InGameSystemがないです");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //時間ないですごめんなさい
        if(other.gameObject.tag == "Player")
        {
            _inGameSystem.ScoreSystem.GameResult();
            SceneManager.LoadScene("ResultScene");
        }
    }
}
