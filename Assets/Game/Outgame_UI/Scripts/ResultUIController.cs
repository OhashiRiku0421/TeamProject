using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System;

public class ResultUIController : MonoBehaviour, IPause
{
    [SerializeField]
    private GameObject[] _uiElements;

    [SerializeField, Tooltip("手にいれたアイテムの数を表示するテキスト")]
    private Text _itemCountText;

    [SerializeField, Tooltip("ベットしたライフを表示するテキスト")]
    private Text _betLifeText;

    [SerializeField, Tooltip("手に入ったライフを表示するテキスト")]
    private Text _gainedLifeText;

    [SerializeField, Tooltip("今までに手に入れたライフを表示するテキスト")]
    private Text _lifeGainedSoFarText;

    [SerializeField, Tooltip("目標のライフを表示するテキスト")]
    private Text _lifeUntilClearText;

    [SerializeField, Tooltip("クリアした時間（分）")]
    private Text _clearTimeMin;

    [SerializeField, Tooltip("クリアした時間（秒）")]
    private Text _clearTimeSec;

    [SerializeField, Tooltip("アニメーションが終わるまでの時間")]
    private float _durationTime = 1f;

    private List<(Text, int)> _textAndValue;
    private float _clearTime;

    [SerializeField]
    private bool _isTest = false;
    void Start()
    {
        int itemCount = ScoreSystem.Score.ItemCount;
        _clearTime = ScoreSystem.Score.ClearTime;
        int betLife = ScoreSystem.Score.BetLife;
        int playerLife = ScoreSystem.Score.PLayerLife;

        if (_isTest)
        {
            itemCount = 5;
            _clearTime = 900;
            betLife = 500;
            playerLife = 200;
        }

        int gainedLife = betLife * itemCount + playerLife;
        ExternalLifeManager.TortalLife += gainedLife;
        _lifeUntilClearText.text = 5000.ToString();

        _textAndValue = new List<(Text, int)>() { (_itemCountText, itemCount), (_betLifeText, betLife), (_gainedLifeText, gainedLife), (_lifeGainedSoFarText, ExternalLifeManager.TortalLife) };
        UpdateTextValue(_textAndValue, 0);
    }

    private void UpdateTextValue(List<(Text, int)> data, int index)
    {
        if (data.Count == index) StartCoroutine(UpdateClearTime(_clearTime));
        Text text = data[index].Item1;
        int value = data[index].Item2;
        int temp = int.Parse(text.text);
        text.DOCounter(temp, value, _durationTime).OnComplete(() => UpdateTextValue(data, ++index));
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

            yield return null;
        }

        _clearTimeMin.text = (Math.Floor(clearTime / 60)).ToString("00");
        _clearTimeSec.text = (Math.Floor(clearTime % 60)).ToString("00");
    }
    public void Resume()
    {

    }
    public void Pause()
    {

    }
}
