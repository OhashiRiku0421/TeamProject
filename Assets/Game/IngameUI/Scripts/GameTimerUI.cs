using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameTimerUI : MonoBehaviour
{
    [Tooltip("分")]
    [SerializeField]
    private Text _timerTextMin;

    [Tooltip("秒")]
    [SerializeField]
    private Text _timerTextSec;

    public void Intialized()
    {
        _timerTextMin.text = "00";
        _timerTextSec.text = "00";
    }

    /// <summary>
    /// 現在のタイムを表示する
    /// </summary>
    public void SetTimerView(float time)
    {
        int sec = (int)time;

        _timerTextMin.text = (sec / 60).ToString("00");
        _timerTextSec.text = (sec % 60).ToString("00");
    }
}
