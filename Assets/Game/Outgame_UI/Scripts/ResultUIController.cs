using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResultUIController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] _uiElements;

    [SerializeField, Tooltip("手にいれたアイテムの数を表示するテキスト")]
    private Text _itemCountText;

    [SerializeField, Tooltip("ベットしたライフを表示するテキスト")]
    private Text _betValueText;

    [SerializeField, Tooltip("手に入ったライフを表示するテキスト")]
    private Text _gainedLifeText;

    [SerializeField, Tooltip("今までに手に入ったライフと、目標のライフを表示するテキスト")]
    private Text _tortalLifeText;

    [SerializeField, Tooltip("クリアした時間（分）")]
    private Text _clearTimeMin;

    [SerializeField, Tooltip("クリアした時間（秒）")]
    private Text _clearTimeSec;

    [SerializeField, Tooltip("テキストのアニメーションが終わるまでの時間")]
    private float _durationTime = 1f;

    private List<(Text, int)> _textAndValue;

    [SerializeField]
    private bool _isTest = false;
    void Start()
    {
        int itemCount = ScoreSystem.Score.ItemCount;
        float clearTime = ScoreSystem.Score.ClearTime;
        int betLife = ScoreSystem.Score.BetLife;
        int playerLife = ScoreSystem.Score.PLayerLife;
        if (_isTest)
        {
            itemCount = 20;
            clearTime = 150;
            betLife = 550;
            playerLife = 200;
        }

        int gainedLife = betLife * itemCount + playerLife;
        ExternalLifeManager.TortalLife = gainedLife;

        _tortalLifeText.text = $"{gainedLife} / 5000";

        _textAndValue = new List<(Text, int)>() { (_itemCountText, itemCount), (_betValueText, betLife), (_gainedLifeText, gainedLife) };
        UpdateTextValue(_itemCountText, itemCount);
        UpdateTextValue(_betValueText, betLife);
        UpdateTextValue(_gainedLifeText, gainedLife);

        StartCoroutine(UpdateClearTime(clearTime));
    }

    private void UpdateTextValue(Text text,  int value)
    {
        int temp = int.Parse(text.text);
        text.DOCounter(temp, value, _durationTime);
        text.text = value.ToString();
    }

    IEnumerator UpdateClearTime(float clearTime)
    {
        float count = 0;
        int sec = 0;
        int min = 0;
        float interval = _durationTime / clearTime; 

        while (_durationTime >= count)
        {
            count += interval;
            _clearTimeSec.text = sec++.ToString("00");

            if (sec >= 60)
            {
                min++;
                _clearTimeMin.text = min.ToString("00");
                sec = 0;
            }

            yield return new WaitForSeconds(interval);
        }

        _clearTimeMin.text = (Math.Floor(clearTime / 60)).ToString("00");
        _clearTimeSec.text = (Math.Floor(clearTime % 60)).ToString("00");
    }
}
