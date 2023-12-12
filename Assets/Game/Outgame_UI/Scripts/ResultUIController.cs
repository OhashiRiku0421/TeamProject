using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.InputSystem;

public class ResultUIController : MonoBehaviour, IPause
{
    /// <summary>ゲーム全体の時間（秒）</summary>
    private float _gameTime = 1200f;

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

    [SerializeField, Tooltip("テスト用の変数")]
    private bool _isTest = false;

    private InputAction _keyPressAction;

    [SerializeField, Tooltip("ライフベットシーンに行くためのボタン")]
    private GameObject _button;

    private bool _isSkip = false;
    private bool _isPause = false;
    Tweener _tweener;

    private void Awake()
    {
        _keyPressAction = new InputAction("AnyKeyPress", InputActionType.Button);
        _keyPressAction.AddBinding("<mouse>/press");
        _keyPressAction.AddBinding("<mouse>/rightButton");
        _keyPressAction.AddBinding("<Keyboard>/anyKey");
        _keyPressAction.performed += OnKeyPressed;
        _button.GetComponent<Button>().interactable = false;
    }
    void Start()
    {
        int itemCount = ScoreSystem.Score.ItemCount;
        _clearTime = ScoreSystem.Score.ClearTime;
        int betLife = ScoreSystem.Score.BetLife;
        int playerLife = ScoreSystem.Score.PLayerLife;

        if (_isTest)
        {
            itemCount = 5;
            _clearTime = 300;
            betLife = 950;
            playerLife = 200;
        }

        int gainedLife = betLife * itemCount + playerLife;
        ExternalLifeManager.TotalLife += gainedLife;
        _lifeUntilClearText.text = 5000.ToString();
        _clearTimeMin.text = (_gameTime / 60).ToString("00");
        _clearTimeSec.text = (_gameTime % 60).ToString("00");

        _textAndValue = new List<(Text, int)>() 
        { 
            (_itemCountText, itemCount), 
            (_betLifeText, betLife), 
            (_gainedLifeText, gainedLife), 
            (_lifeGainedSoFarText, ExternalLifeManager.TotalLife)
        };
        
        UpdateTextValue(_textAndValue, 0);
    }

    private void UpdateTextValue(List<(Text, int)> data, int index)
    {
        // スコアの表示が終わったら時間の表示に移る
        if (data.Count == index)
        {
            StartCoroutine(UpdateClearTime());
            return;
        }

        Text text = data[index].Item1;
        int value = data[index].Item2;

        int temp = int.Parse(text.text);
        _tweener = text.DOCounter(temp, value, _durationTime).SetEase(Ease.Linear).OnComplete(() => UpdateTextValue(data, ++index));

        if (DOTween.IsTweening(text) && _isSkip)
        {
            text.DOComplete();
            UpdateTextValue(data, ++index);
        }
    }

    IEnumerator UpdateClearTime()
    {
        float count = 0;
        int sec = (int)Math.Floor(_gameTime % 60);
        int min = (int)Math.Floor(_gameTime / 60);
        float interval = _durationTime / (_gameTime - _clearTime);

        while (_durationTime > count && !_isSkip)
        {
            if (sec <= 0)
            {
                min--;
                sec = 59;
            }

            _clearTimeMin.text = min.ToString("00");
            _clearTimeSec.text = sec.ToString("00");
            sec--;
            count += interval;

            yield return new WaitUntil(() => { return _isPause ? false : true; });
        }

        _clearTimeMin.text = (Math.Floor(_clearTime / 60)).ToString("00");
        _clearTimeSec.text = (Math.Floor(_clearTime % 60)).ToString("00");

        _button.GetComponent<Button>().interactable = true;
    }

    public void OnKeyPressed(InputAction.CallbackContext context)
    {
        // _isSkip = true;
        Debug.Log("IsSkip");
    }
    private void OnEnable()
    {
        _keyPressAction.Enable();
    }

    private void OnDisable()
    {
        _keyPressAction.Disable();
    }

    public void Resume()
    {
        _isPause = false;
        _tweener?.Play();
    }

    public void Pause()
    {
        _isPause = true;
        _tweener?.Pause();
    }
}
