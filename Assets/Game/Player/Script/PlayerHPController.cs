using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlayerHPController : MonoBehaviour, IDamage
{
    [SerializeField, Tooltip("ゴッドモード")] private bool _isGodMode = false;

    [SerializeField, Tooltip("ゴッドモードHP")] private float _godModeHP = 10000F;

    /// <summary>現在のHP</summary>
    private float _currentHP = 0F;

    private float _maxHP;

    /// <summary>CurrentHPが変更された際に呼ばれる</summary>
    private Action<float> _onCurrentHPChanged = default;

    private Subject<Unit> _hitVoice = new();

    /// <summary>現在のHP</summary>
    public float CurrentHP
    {
        get => _currentHP;
        private set
        {
            if (_currentHP != value)
            {
                _onCurrentHPChanged?.Invoke(value);
                _currentHP = value;
            }
        }
    }

    /// <summary>CurrentHPが変更された際に呼ばれる</summary>
    public event Action<float> OnCurrentHpChanged
    {
        add => _onCurrentHPChanged += value;
        remove => _onCurrentHPChanged -= value;
    }

    /// <summary>プレイヤーのHPが0になった際の処理</summary>
    private Action _onDeadEvent = default;

    /// <summary>プレイヤーのHPが0になった際の処理</summary>
    public event Action OnDeadEvent
    {
        add => _onDeadEvent += value;
        remove => _onDeadEvent -= value;
    }

    private void Start()
    {
        if (_isGodMode)
        {
            CurrentHP = _godModeHP;
            _maxHP = _godModeHP;
        }
        else
        {
            _maxHP = ExternalLifeManager.Life;
            CurrentHP = ExternalLifeManager.Life;
        }
        Debug.Log(ExternalLifeManager.Life);
        _hitVoice.FirstOrDefault(_ => _currentHP <= _currentHP / 2 && _currentHP > _currentHP / 5)
            .Subscribe(_ => CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_HP_Half"));
        _hitVoice.FirstOrDefault(_ => _currentHP <= _currentHP / 5)
            .Subscribe(_ => CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_HP_Dying"));
    }


    public void SendDamage(float damage)
    {
        CurrentHP -= damage;

        // 死んだ際の処理
        if (_currentHP <= 0)
        {
            _onDeadEvent?.Invoke();
            CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Dead");
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Dead");
        }
        else
        {
            CriAudioManager.Instance.SE.Play("VOICE", "VC_Player_Damage");
            CriAudioManager.Instance.SE.Play("SE", "SE_Player_Damage");
            _hitVoice.OnNext(Unit.Default);
        }
    }
}